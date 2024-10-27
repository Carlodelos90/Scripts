using System.Collections.Generic;
using UnityEngine;

public class AudioClipLoader : MonoBehaviour
{
    public List<string> folderNames; // List of folder paths to load audio clips from

    public List<AudioClip> GetAudioClips(string folderPath)
    {
        List<AudioClip> audioClips = new List<AudioClip>();

        // Load all audio clips from the specified folder inside the Resources folder
        AudioClip[] clips = Resources.LoadAll<AudioClip>(folderPath);
        if (clips.Length > 0)
        {
            foreach (AudioClip clip in clips)
            {
                audioClips.Add(clip);
                Debug.Log("Loaded audio clip: " + clip.name);
            }
        }
        else
        {
            Debug.LogError("Failed to load audio clips for folder: " + folderPath);
        }

        return audioClips;
    }

    public List<AudioClip> LoadLevelAudioClips(int level)
    {
        if (level < 0 || level >= folderNames.Count)
        {
            Debug.LogError("Invalid level number: " + level);
            return new List<AudioClip>();
        }

        string folderPath = folderNames[level];
        Debug.Log("Loading audio clips from folder: " + folderPath);
        return GetAudioClips(folderPath);
    }
}
