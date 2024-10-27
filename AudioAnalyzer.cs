using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class AudioAnalyzer : MonoBehaviour
{
    public float sensitivity = 100.0f;
    public float threshold = 0.02f;
    private AudioSource audioSource;
    private float[] samples = new float[1024];
    private List<float> beatTimes = new List<float>();

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman);
        float sum = 0;
        for (int i = 0; i < samples.Length; i++)
        {
            sum += samples[i];
        }

        if (sum > threshold)
        {
            beatTimes.Add(Time.time);
        }

        // Clean up old beat times
        beatTimes.RemoveAll(time => Time.time - time > 1.0f / sensitivity);
    }

    public bool IsBeat()
    {
        return beatTimes.Count > 0;
    }
}
