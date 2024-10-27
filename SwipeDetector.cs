using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class SwipeDetector : MonoBehaviour
{
    private AudioSource audioSource;
    private BallControl ballControl;
    private int currentNoteIndex = 0; // Track the current note index
    private List<AudioClip> currentLoadedClips;

    public AudioClipLoader audioClipLoader; // Reference to AudioClipLoader
    public float moveDistance = 8.0f; // Distance the ball moves with each input

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("Missing AudioSource component on the SwipeDetector GameObject.");
        }

        ballControl = FindObjectOfType<BallControl>();
        if (ballControl == null)
        {
            Debug.LogError("BallControl script not found in the scene.");
        }

        if (audioClipLoader == null)
        {
            audioClipLoader = GetComponent<AudioClipLoader>();
            if (audioClipLoader == null)
            {
                Debug.LogError("AudioClipLoader script not found on the SwipeDetector GameObject.");
            }
            else
            {
                Debug.Log("AudioClipLoader found and assigned.");
            }
        }
    }

    private void Update()
    {
        DetectMouseClick();
        DetectKeyboardInput();
    }

    private void DetectMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 clickPosition = Input.mousePosition;
            clickPosition.z = Camera.main.transform.position.y - ballControl.transform.position.y; // Set Z based on the camera distance

            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(clickPosition);
            worldPosition.z = ballControl.transform.position.z; // Keep the z position unchanged

            Vector3 ballPosition = ballControl.transform.position;
            Vector3 direction = worldPosition - ballPosition;
            direction.y = 0; // Keep the y position unchanged

            ballControl.MoveBall(new Vector2(direction.x, direction.z).normalized * moveDistance);
            PlayNextNote();
        }
    }

    private void DetectKeyboardInput()
    {
        Vector2 direction = Vector2.zero;

        if (Input.GetKeyDown(KeyCode.A))
        {
            direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            direction = Vector2.right;
        }

        if (direction != Vector2.zero)
        {
            ballControl.MoveBall(direction * moveDistance);
            PlayNextNote();
        }
    }

    private void PlayNextNote()
    {
        if (currentLoadedClips == null)
        {
            Debug.LogWarning("No swipe notes loaded: currentLoadedClips is null.");
            return;
        }

        if (currentLoadedClips.Count == 0)
        {
            Debug.LogWarning("No swipe notes loaded: currentLoadedClips is empty.");
            return;
        }

        if (currentNoteIndex >= currentLoadedClips.Count)
        {
            currentNoteIndex = 0; // Loop back to the first note
        }

        Debug.Log($"Playing note {currentNoteIndex}: {currentLoadedClips[currentNoteIndex].name}");
        audioSource.PlayOneShot(currentLoadedClips[currentNoteIndex]);
        currentNoteIndex++;
    }

    public void LoadLevelAudioClips(string folderName)
    {
        Debug.Log("Loading audio clips for folder: " + folderName);
        currentLoadedClips = audioClipLoader.GetAudioClips(folderName);

        if (currentLoadedClips != null && currentLoadedClips.Count > 0)
        {
            // Sort the clips by name to ensure they play in the correct order
            currentLoadedClips.Sort((clip1, clip2) => string.Compare(clip1.name, clip2.name));

            Debug.Log("Loaded and sorted " + currentLoadedClips.Count + " clips for folder " + folderName);
        }
        else
        {
            Debug.LogError("Failed to load audio clips for folder: " + folderName);
        }

        currentNoteIndex = 0; // Reset note index for the new level
    }
}
