using UnityEngine;

public class CameraIntro : MonoBehaviour
{
    public Transform ball; // Assign the ball transform in the Inspector
    public float introSpeed = 50.0f; // Speed of the camera intro movement
    public float followDistance = 10.0f; // Distance behind the ball to follow
    public float followHeight = 10.0f; // Height above the ball to follow
    public float followSpeed = 5.0f; // Speed at which the camera follows the ball
    public float startY = 400.0f; // Starting Y position of the camera

    private bool isIntroComplete = false;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.Play(); // Play the intro sound effect
        }

        // Set the starting position of the camera
        transform.position = new Vector3(ball.position.x, startY, ball.position.z);
    }

    void Update()
    {
        if (!isIntroComplete)
        {
            IntroMovement();
        }
        else
        {
            FollowBall();
        }
    }

    void IntroMovement()
    {
        // Move the camera downwards along the Y-axis
        float step = introSpeed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, Mathf.MoveTowards(transform.position.y, ball.position.y + followHeight, step), transform.position.z);

        // Check if the camera has reached the follow height above the ball
        if (Mathf.Abs(transform.position.y - (ball.position.y + followHeight)) < 0.1f)
        {
            isIntroComplete = true;
        }
    }

    void FollowBall()
    {
        // Smoothly follow the ball
        Vector3 followPosition = ball.position + new Vector3(0, followHeight, -followDistance);
        transform.position = Vector3.Lerp(transform.position, followPosition, Time.deltaTime * followSpeed);
        transform.LookAt(ball);
    }
}
