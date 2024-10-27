using UnityEngine;
using UnityEngine.UI;

public class LoudnessDetector : MonoBehaviour
{
    public AudioSource audioSource;
    public float updateStep = 0.1f; // Time interval to update the loudness
    public int sampleDataLength = 1024; // Number of samples to analyze
    public float loudnessMultiplier = 100.0f; // Multiplier for visual effects

    private float currentUpdateTime = 0f;
    private float[] clipSampleData;
    private float currentLoudness = 0f;

    public Text scoreText; // Reference to the ScoreText
    public Image heart1; // Reference to Heart 1
    public Image heart2; // Reference to Heart 2
    public Image heart3; // Reference to Heart 3

    void Awake()
    {
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is not assigned.");
        }

        if (scoreText == null)
        {
            Debug.LogError("ScoreText is not assigned.");
        }

        if (heart1 == null || heart2 == null || heart3 == null)
        {
            Debug.LogError("Heart images are not assigned.");
        }

        clipSampleData = new float[sampleDataLength];
    }

    void Update()
    {
        currentUpdateTime += Time.deltaTime;
        if (currentUpdateTime >= updateStep)
        {
            currentUpdateTime = 0f;
            audioSource.GetOutputData(clipSampleData, 0); // Get raw audio data
            float clipLoudness = 0f;
            foreach (var sample in clipSampleData)
            {
                clipLoudness += Mathf.Abs(sample);
            }
            clipLoudness /= sampleDataLength;
            currentLoudness = clipLoudness * loudnessMultiplier;
            ApplyVisualEffects(currentLoudness); // Apply visual effects based on loudness
        }
    }

    void ApplyVisualEffects(float loudness)
    {
        // Example: Change the scale of the score text based on loudness
        if (scoreText != null)
        {
            float scale = 1 + loudness * 0.1f; // Adjust the multiplier to control the effect
            scoreText.transform.localScale = new Vector3(scale, scale, scale);
        }

        // Apply the same effect to the hearts
        ApplyHeartPulse(heart1, loudness);
        ApplyHeartPulse(heart2, loudness);
        ApplyHeartPulse(heart3, loudness);
    }

    void ApplyHeartPulse(Image heart, float loudness)
    {
        if (heart != null)
        {
            float scale = 1 + loudness * 0.1f; // Adjust the multiplier to control the effect
            heart.transform.localScale = new Vector3(scale, scale, scale);
        }
    }

    public float GetCurrentLoudness()
    {
        return currentLoudness;
    }
}
