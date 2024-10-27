using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

[System.Serializable]
public class LevelData
{
    public string sceneName;  // The actual scene name
    public string displayName; // The name to display on the button
}

public class LevelSelectionManager : MonoBehaviour
{
    public GameObject levelButtonTemplate; // Assign the button template in the Inspector
    public RectTransform content; // Assign the Content RectTransform in the Inspector
    public List<LevelData> levels; // List of levels with scene name and display name

    void Start()
    {
        PopulateLevelButtons();
    }

    void PopulateLevelButtons()
    {
        levelButtonTemplate.SetActive(false); // Hide the template initially

        foreach (LevelData level in levels)
        {
            GameObject button = Instantiate(levelButtonTemplate, content);
            button.SetActive(true);
            button.GetComponentInChildren<Text>().text = level.displayName;
            button.GetComponent<Button>().onClick.AddListener(() => LoadLevel(level.sceneName));
        }
    }

    void LoadLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
