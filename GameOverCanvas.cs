using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverCanvas : MonoBehaviour
{
    public Text scoreText;
    public Text timeText;
    public float decelerationRate = 0.1f;
    public AudioSource gameOverAudioSource; // Add this line
    public AudioClip gameOverSound; // Add this line

    private void Start()
    {
        gameObject.SetActive(false); // Ensure the game over canvas is inactive initially
    }

    public void ShowGameOver()
    {
        // Assuming you have a ScoreManager and TimerManager to get the score and time
        if (ScoreManager.instance != null)
        {
            scoreText.text = "Score: " + ScoreManager.instance.GetTotalScore();
        }

        if (timeText != null)
        {
            timeText.text = "Time: " + Time.timeSinceLevelLoad.ToString("F2");
        }

        gameObject.SetActive(true);
        StartCoroutine(PauseGameGradually());

        // Play game over sound
        if (gameOverAudioSource != null && gameOverSound != null)
        {
            gameOverAudioSource.PlayOneShot(gameOverSound);
        }
    }

    private IEnumerator PauseGameGradually()
    {
        while (Time.timeScale > 0.01f)
        {
            Time.timeScale = Mathf.Max(Time.timeScale - decelerationRate * Time.unscaledDeltaTime, 0.01f);
            yield return null;
        }
        Time.timeScale = 0;
    }

    public void HideGameOver()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void WatchAdForExtraLife()
    {
        // Implement logic to show an ad and give an extra life
        Debug.Log("Watch Ad for Extra Life");
        AdsManager adsManager = FindObjectOfType<AdsManager>();
        if (adsManager != null)
        {
            adsManager.ShowAd("YOUR_AD_UNIT_ID"); // Replace with your actual ad unit ID
        }
    }
}
