using UnityEngine;

public class VisualBallEffect : MonoBehaviour
{
    public float rotationSpeedX = 10.0f; // Speed of rotation around the X axis
    public float rotationSpeedY = 10.0f; // Speed of rotation around the Y axis
    public float rotationSpeedZ = 10.0f; // Speed of rotation around the Z axis
    public float movementAmplitude = 0.1f; // Amplitude of the subtle movement
    public float movementFrequency = 1.0f; // Frequency of the subtle movement

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.localPosition;
    }

    void Update()
    {
        // Subtle rotation effect
        transform.Rotate(rotationSpeedX * Time.deltaTime, rotationSpeedY * Time.deltaTime, rotationSpeedZ * Time.deltaTime);

        // Subtle movement effect
        float movementOffset = Mathf.Sin(Time.time * movementFrequency) * movementAmplitude;
        transform.localPosition = startPosition + new Vector3(0, movementOffset, 0);
    }
}
