using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    public int lives = 3; //amount of lives
    public Image heart1;
    public Image heart2;
    public Image heart3;
    public float fadeDuration = 1.0f;
    public AudioClip loseLifeSound; // Sound to play when losing a life
    public Color loseLifeColor = Color.red; // Color to change to when losing a life
    public float colorChangeDuration = 0.5f; // Duration of the color change

    private Image[] hearts;
    private AudioSource audioSource;
    private ShaderEffect_CRT shaderEffectCRT;

    void Start()
    {
        hearts = new Image[] { heart1, heart2, heart3 };
        audioSource = GetComponent<AudioSource>();
        shaderEffectCRT = Camera.main.GetComponent<ShaderEffect_CRT>();
        UpdateHeartsDisplay();
    }

    public void LoseLife()
    {
        if (lives > 0)
        {
            lives--;
            StartCoroutine(FadeHeart(hearts[lives]));
            PlayLoseLifeSound();
            StartCoroutine(ChangeShaderEffect());
        }

        if (lives <= 0)
        {
            // Handle game over logic here
            Debug.Log("Game Over!");
            GameManager.Instance.GameOver();
        }

        UpdateHeartsDisplay();
    }

    public void AddLife()
    {
        if (lives < hearts.Length)
        {
            lives+= 2;
            StartCoroutine(ShowHeart(hearts[lives - 1]));
        }
        UpdateHeartsDisplay();
    }

    private void PlayLoseLifeSound()
    {
        if (audioSource != null && loseLifeSound != null)
        {
            audioSource.PlayOneShot(loseLifeSound);
        }
    }

    private IEnumerator ChangeShaderEffect()
    {
        if (shaderEffectCRT != null)
        {
            float initialIntensity = shaderEffectCRT.scanlineIntensity;
            float targetIntensity = initialIntensity * 2; // Example: Double the intensity

            int initialWidth = shaderEffectCRT.scanlineWidth;
            int targetWidth = initialWidth + 2; // Example: Increase width by 2

            float elapsedTime = 0.0f;

            while (elapsedTime < colorChangeDuration)
            {
                shaderEffectCRT.SetScanlineIntensity(Mathf.Lerp(initialIntensity, targetIntensity, elapsedTime / colorChangeDuration));
                shaderEffectCRT.SetScanlineWidth(Mathf.RoundToInt(Mathf.Lerp(initialWidth, targetWidth, elapsedTime / colorChangeDuration)));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            shaderEffectCRT.SetScanlineIntensity(targetIntensity);
            shaderEffectCRT.SetScanlineWidth(targetWidth);

            // Wait for a moment with the increased effect
            yield return new WaitForSeconds(colorChangeDuration);

            // Revert to the original values
            elapsedTime = 0.0f;
            while (elapsedTime < colorChangeDuration)
            {
                shaderEffectCRT.SetScanlineIntensity(Mathf.Lerp(targetIntensity, initialIntensity, elapsedTime / colorChangeDuration));
                shaderEffectCRT.SetScanlineWidth(Mathf.RoundToInt(Mathf.Lerp(targetWidth, initialWidth, elapsedTime / colorChangeDuration)));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            shaderEffectCRT.SetScanlineIntensity(initialIntensity);
            shaderEffectCRT.SetScanlineWidth(initialWidth);
        }
    }

    private IEnumerator FadeHeart(Image heart)
    {
        float elapsedTime = 0.0f;
        Color originalColor = heart.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            heart.color = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.Lerp(1, 0, elapsedTime / fadeDuration));
            yield return null;
        }

        heart.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);
    }

    private IEnumerator ShowHeart(Image heart)
    {
        float elapsedTime = 0.0f;
        Color originalColor = heart.color;
        originalColor.a = 0; // Ensure alpha starts at 0
        heart.color = originalColor;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            heart.color = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.Lerp(0, 1, elapsedTime / fadeDuration));
            yield return null;
        }

        heart.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1);
    }

    private void UpdateHeartsDisplay()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].color = i < lives ? new Color(hearts[i].color.r, hearts[i].color.g, hearts[i].color.b, 1) : new Color(hearts[i].color.r, hearts[i].color.g, hearts[i].color.b, 0);
        }
    }
}
