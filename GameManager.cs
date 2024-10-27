using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton instance
    public SwipeDetector swipeDetector;
    public GameOverCanvas gameOverCanvas;

    private void Awake()
    {
        // Implement singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Ensure swipeDetector is assigned
        if (swipeDetector == null)
        {
            swipeDetector = FindObjectOfType<SwipeDetector>();
            if (swipeDetector == null)
            {
                Debug.LogError("SwipeDetector script not found in the scene.");
                return;
            }
        }

        // Load the initial level audio clips based on the current scene name
        LoadLevelAudio(SceneManager.GetActiveScene().name);

        // Ensure gameOverCanvas is assigned and initially inactive
        if (gameOverCanvas == null)
        {
            gameOverCanvas = FindObjectOfType<GameOverCanvas>();
            if (gameOverCanvas == null)
            {
                Debug.LogError("GameOverCanvas script not found in the scene.");
                return;
            }
        }
        gameOverCanvas.gameObject.SetActive(false); // Ensure the game over canvas is inactive initially
    }

    public void LoadLevel(string sceneName)
    {
        // Load the corresponding scene
        SceneManager.LoadScene(sceneName);
    }

    public void LoadLevelAudio(string sceneName)
    {
        switch (sceneName)
        {
            case "HappyBirthday":
                swipeDetector.LoadLevelAudioClips("Audio/HappyBirthday");
                break;
            case "JingleBells":
                swipeDetector.LoadLevelAudioClips("Audio/JingleBells");
                break;
            case "Hallking":
                swipeDetector.LoadLevelAudioClips("Audio/Hallking");
                break;
            // Add more cases for additional scenes
            default:
                Debug.LogError("Invalid scene name: " + sceneName);
                break;
        }
    }

    // Call this method when a level button is clicked
    public void OnLevelButtonClicked(string sceneName)
    {
        LoadLevelAudio(sceneName);
        LoadLevel(sceneName);
    }

    public void GameOver()
    {
        if (gameOverCanvas != null)
        {
            gameOverCanvas.ShowGameOver();
        }
    }

    public void ResumeGame()
    {
        // Logic to resume the game after watching an ad
        Time.timeScale = 1; // Resume game time
        GameOverCanvas gameOverCanvas = FindObjectOfType<GameOverCanvas>();
        if (gameOverCanvas != null)
        {
            gameOverCanvas.HideGameOver();
        }
    }

    public void OnAdWatched()
    {
        // Give an extra life when the ad is successfully watched
        LifeManager lifeManager = FindObjectOfType<LifeManager>();
        if (lifeManager != null)
        {
            lifeManager.AddLife();
        }

        // Resume the game
        ResumeGame();
    }

    // Update life icons method to handle lives UI update
    public void UpdateLifeIcons(int lives)
    {
        // Your logic to update life icons goes here
    }
}
