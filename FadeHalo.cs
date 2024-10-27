using UnityEngine;
using System.Collections;

public class FadeHalo : MonoBehaviour
{
    public float fadeDuration = 1.0f;  // Duration of the fade effect in seconds
    private Renderer haloRenderer;
    private Color originalColor;

    void Start()
    {
        haloRenderer = GetComponent<Renderer>();
        originalColor = haloRenderer.material.color;
        SetHaloAlpha(0.0f);  // Start with the halo invisible
    }

    public void FadeIn()
    {
        StartCoroutine(FadeInCoroutine());
    }

    private IEnumerator FadeInCoroutine()
    {
        float elapsedTime = 0.0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            SetHaloAlpha(alpha);
            yield return null;
        }

        SetHaloAlpha(1.0f);  // Ensure the final alpha is set to 1
    }

    private void SetHaloAlpha(float alpha)
    {
        Color color = originalColor;
        color.a = alpha;
        haloRenderer.material.color = color;
    }
}
