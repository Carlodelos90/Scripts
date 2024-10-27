using UnityEngine;
using UnityEngine.UI;

public class LastPlatformTrigger : MonoBehaviour
{
    public GameObject gameDetailsCanvas; // Assign the GameDetailsCanvas in the Inspector
    public GameTimer gameTimer; // Reference to the GameTimer script

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Assuming the player has a tag named "Player"
        {
            Debug.Log("Player entered trigger"); // Debug log
            gameTimer.StopTimer(); // Stop the timer
            Debug.Log("Timer stopped"); // Debug log

            Text timerText = gameDetailsCanvas.GetComponentInChildren<Text>();
            if (timerText != null)
            {
                timerText.text = "Time: " + gameTimer.GetElapsedTime(); // Display the elapsed time
                Debug.Log("Elapsed time displayed: " + gameTimer.GetElapsedTime()); // Debug log
            }
            gameDetailsCanvas.SetActive(true); // Activate the game details canvas
            Debug.Log("Game details canvas activated"); // Debug log
        }
    }
}
