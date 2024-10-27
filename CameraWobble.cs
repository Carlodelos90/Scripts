using UnityEngine;

public class CameraWobble : MonoBehaviour
{
    public float positionAmplitude = 0.1f; // Amplitude of the position wobble
    public float positionFrequency = 1.0f; // Frequency of the position wobble
    public float rotationAmplitude = 1.0f; // Amplitude of the rotation wobble
    public float rotationFrequency = 1.0f; // Frequency of the rotation wobble

    private Vector3 startPosition;
    private Quaternion startRotation;

    void Start()
    {
        startPosition = transform.localPosition;
        startRotation = transform.localRotation;
    }

    void Update()
    {
        // Apply position wobble
        float posOffsetX = Mathf.Sin(Time.time * positionFrequency) * positionAmplitude;
        float posOffsetY = Mathf.Cos(Time.time * positionFrequency) * positionAmplitude;
        float posOffsetZ = Mathf.Sin(Time.time * positionFrequency * 0.5f) * positionAmplitude; // Different frequency for Z

        transform.localPosition = startPosition + new Vector3(posOffsetX, posOffsetY, posOffsetZ);

        // Apply rotation wobble
        float rotOffsetX = Mathf.Sin(Time.time * rotationFrequency) * rotationAmplitude;
        float rotOffsetY = Mathf.Cos(Time.time * rotationFrequency) * rotationAmplitude;
        float rotOffsetZ = Mathf.Sin(Time.time * rotationFrequency * 0.5f) * rotationAmplitude; // Different frequency for Z

        transform.localRotation = startRotation * Quaternion.Euler(rotOffsetX, rotOffsetY, rotOffsetZ);
    }
}
