using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class BallControl : MonoBehaviour
{
    public float moveSpeed = 10.0f; // Speed of the ball movement
    public float moveDistance = 8.0f; // Distance to move per tap
    public float boostedSpeed = 20.0f; // Speed during boost
    public float boostDuration = 2.0f; // Duration of the boost
    public float fallSpeed = 2.0f; // Speed of falling in the Y axis
    public float speedIncreaseRate = 0.1f; // Rate of speed increase over time
    public Text speedometerText; // Assign this in the Inspector

    private Rigidbody rb;
    public AudioClip hitSound; // Make sure to assign this in the Inspector
    private AudioSource audioSource;

    private bool isBoosting = false;
    private float boostEndTime;
    private bool isMoving = false;
    private Vector3 targetPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("Missing AudioSource component on the ball GameObject.");
        }
        StartCoroutine(IncreaseSpeeds());
    }

    void OnCollisionEnter(Collision collision)
    {
        if (audioSource != null && hitSound != null && collision.gameObject.CompareTag("Platform"))
        {
            audioSource.PlayOneShot(hitSound);
        }
        else if (hitSound == null)
        {
            Debug.LogWarning("Hit sound AudioClip is not assigned on the BallControl script.");
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            // Player missed the halo, reset or handle the miss
            ScoreManager.instance.AddScore(0);
        }
    }

    void Update()
    {
        ApplyFall();

        if (isBoosting)
        {
            if (Time.time >= boostEndTime)
            {
                isBoosting = false;
                Debug.Log("Boost ended");
            }
            else
            {
                Vector3 velocity = rb.velocity;
                velocity.y = boostedSpeed; // Apply boost speed only to the Y axis
                rb.velocity = velocity;
                Debug.Log("Boost active, current speed: " + boostedSpeed);
            }
        }

        if (isMoving)
        {
            MoveBall();
        }

        UpdateSpeedometer();
    }

    private void ApplyFall()
    {
        Vector3 velocity = rb.velocity;
        velocity.y = -fallSpeed;
        rb.velocity = velocity;
    }

    public void MoveBall(Vector2 direction)
    {
        Vector3 moveDirection = new Vector3(direction.x, 0.0f, direction.y).normalized;
        targetPosition = transform.position + moveDirection * moveDistance;
        isMoving = true;
    }

    private void MoveBall()
    {
        Vector3 newPosition = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        newPosition.y = transform.position.y; // Keep the Y position unchanged
        transform.position = newPosition;

        if (Vector3.Distance(transform.position, targetPosition) < 0.001f)
        {
            isMoving = false;
        }
    }

    public void ActivateBoost()
    {
        isBoosting = true;
        boostEndTime = Time.time + boostDuration;
        Debug.Log("Boost activated, boost end time: " + boostEndTime);
    }

    private IEnumerator IncreaseSpeeds()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f); // Increase speeds every second
            fallSpeed += speedIncreaseRate;
            moveSpeed += speedIncreaseRate;
        }
    }

    void UpdateSpeedometer()
    {
        if (speedometerText != null)
        {
            float speed = rb.velocity.magnitude;
            float randomFluctuation = Random.Range(-1f, 1f) * 0.1f;
            float displayedSpeed = Mathf.Clamp(speed + randomFluctuation, 0, float.MaxValue);
            speedometerText.text = $"Speed: {displayedSpeed:F2}";
        }
    }
}
