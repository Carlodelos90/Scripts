using UnityEngine;
using RhythmTool;

public class BeatAnalyzer : MonoBehaviour
{
    public AudioClip audioClip;
    private RhythmAnalyzer analyzer;
    public static RhythmData rhythmData;

    void Start()
    {
        analyzer = GetComponent<RhythmAnalyzer>();
        if (analyzer != null && audioClip != null)
        {
            rhythmData = analyzer.Analyze(audioClip);
        }
    }
}
