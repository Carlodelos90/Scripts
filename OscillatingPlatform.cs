using UnityEngine;

public class OscillatingPlatform : MonoBehaviour
{
    public enum OscillationType { Horizontal, Vertical, Diagonal, Random }

    public OscillationType oscillationType = OscillationType.Horizontal;
    public float amplitude = 1.0f; // How far the platform moves from its start position
    public float frequency = 1.0f; // How fast the platform oscillates

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float oscillation = Mathf.Sin(Time.time * frequency) * amplitude;

        switch (oscillationType)
        {
            case OscillationType.Horizontal:
                transform.position = startPosition + new Vector3(oscillation, 0, 0);
                break;
            case OscillationType.Vertical:
                transform.position = startPosition + new Vector3(0, oscillation, 0);
                break;
            case OscillationType.Diagonal:
                transform.position = startPosition + new Vector3(oscillation, oscillation, 0);
                break;
            case OscillationType.Random:
                float randomX = Mathf.Sin(Time.time * frequency) * amplitude;
                float randomY = Mathf.Cos(Time.time * frequency) * amplitude;
                transform.position = startPosition + new Vector3(randomX, randomY, 0);
                break;
        }
    }
}
