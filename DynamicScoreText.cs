using UnityEngine;
using UnityEngine.UI;
using TMPro; // Assuming you're using TextMeshPro for UI text elements
using System.Collections;

public class DynamicScoreText : MonoBehaviour
{
    public TextMeshProUGUI dynamicScoreText; // Assign this in the Inspector
    public float pulseDuration = 0.5f; // Duration of the pulse effect
    public float fadeDuration = 1.0f; // Duration of the fade effect
    private float targetScale = 1.5f; // Target scale for the pulse effect

    private Color originalColor;
    private Vector3 originalScale;
    private Coroutine fadeCoroutine;

    void Start()
    {
        if (dynamicScoreText == null)
        {
            Debug.LogError("DynamicScoreText is not assigned.");
            return;
        }

        originalColor = dynamicScoreText.color;
        originalScale = dynamicScoreText.transform.localScale;
        dynamicScoreText.gameObject.SetActive(false); // Hide the dynamic score text initially
    }

    public void ShowDynamicScore(int score)
    {
        if (dynamicScoreText == null)
        {
            Debug.LogError("DynamicScoreText is not assigned.");
            return;
        }

        dynamicScoreText.text = "+" + score;
        dynamicScoreText.gameObject.SetActive(true); // Show the dynamic score text

        // Start the pulse effect
        StopAllCoroutines();
        StartCoroutine(PulseEffect());

        // Start the fade effect
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine(FadeOutEffect());
    }

    private IEnumerator PulseEffect()
    {
        float elapsedTime = 0f;
        while (elapsedTime < pulseDuration)
        {
            float scale = Mathf.Lerp(1.0f, targetScale, elapsedTime / pulseDuration);
            dynamicScoreText.transform.localScale = originalScale * scale;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        dynamicScoreText.transform.localScale = originalScale;
    }

    private IEnumerator FadeOutEffect()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            Color color = originalColor;
            color.a = Mathf.Lerp(1.0f, 0f, elapsedTime / fadeDuration);
            dynamicScoreText.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        dynamicScoreText.color = originalColor;
        dynamicScoreText.gameObject.SetActive(false); // Hide the dynamic score text after fade out
    }

    public void ChangeColor(Color newColor)
    {
        originalColor = newColor;
        dynamicScoreText.color = newColor;
    }
}
