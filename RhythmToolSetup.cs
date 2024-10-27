using UnityEngine;
using RhythmTool;
using System.Collections; // Add this line

public class RhythmToolSetup : MonoBehaviour
{
    public AudioClip audioClip;
    public RhythmAnalyzer analyzer;
    public RhythmData rhythmData;

    void Start()
    {
        // Analyze the audio clip
        rhythmData = analyzer.Analyze(audioClip);

        // Wait for analysis to complete
        StartCoroutine(WaitForAnalysis());
    }

    IEnumerator WaitForAnalysis()
    {
        while (!analyzer.isDone)
        {
            yield return null;
        }

        // Analysis complete, you can now use rhythmData
        Debug.Log("Analysis complete!");
    }
}
