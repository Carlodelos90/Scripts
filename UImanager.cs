using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Canvas mainMenuCanvas;
    public Canvas levelSelectionCanvas;
    public Canvas settingsCanvas;

    void Start()
    {
        mainMenuCanvas.gameObject.SetActive(true);
        levelSelectionCanvas.gameObject.SetActive(false);
        settingsCanvas.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene"); // Replace with your actual game scene name
    }

    public void OpenLevelSelection()
    {
        mainMenuCanvas.gameObject.SetActive(false);
        levelSelectionCanvas.gameObject.SetActive(true);
    }

    public void OpenSettings()
    {
        mainMenuCanvas.gameObject.SetActive(false);
        settingsCanvas.gameObject.SetActive(true);
    }

    public void BackToMainMenu()
    {
        levelSelectionCanvas.gameObject.SetActive(false);
        settingsCanvas.gameObject.SetActive(false);
        mainMenuCanvas.gameObject.SetActive(true);
    }

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}
