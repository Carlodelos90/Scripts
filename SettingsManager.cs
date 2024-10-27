using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;
    public Dropdown graphicsQualityDropdown;
    public AudioSource musicSource; // Assign your music audio source

    void Start()
    {
        // Load saved settings
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1.0f);
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1.0f);
        graphicsQualityDropdown.value = PlayerPrefs.GetInt("GraphicsQuality", 2);

        // Add listeners
        musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxVolumeSlider.onValueChanged.AddListener(SetSFXVolume);
        graphicsQualityDropdown.onValueChanged.AddListener(SetGraphicsQuality);
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        // Adjust SFX volume in your game
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    public void SetGraphicsQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("GraphicsQuality", qualityIndex);
    }

    public void BackToMainMenu()
    {
        // Load the main menu scene
        SceneManager.LoadScene("MainMenu");
    }
}
