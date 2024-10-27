using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class ManualLevelSelectionManager : MonoBehaviour
{
    // Public fields for each button, assign in the Inspector
    public Button level1Button;
    public Button level2Button;
    // Add more buttons as needed

    void Start()
    {
        // Add listeners for each button
        level1Button.onClick.AddListener(() => LoadLevel("happybirthday"));
        level2Button.onClick.AddListener(() => LoadLevel("JingleBells"));
        // Add more listeners as needed
    }

    void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}
