using UnityEngine;
using System.Collections;

public class CircleManager : MonoBehaviour
{
    public GameObject[] circles; // Array to hold your circles
    private int currentCircleIndex = 0; // Index to track the current circle
    public float fadeDuration = 2.0f; // Duration for the fade-in effect

    void Start()
    {
        // Disable all circles except the first one
        for (int i = 0; i < circles.Length; i++)
        {
            circles[i].SetActive(false);
        }

        // Activate the first circle with fade-in effect
        if (circles.Length > 0)
        {
            circles[0].SetActive(true);
            StartCoroutine(FadeIn(circles[0]));
        }
    }

    // Method to activate the next circle
    public void ActivateNextCircle()
    {
        if (currentCircleIndex < circles.Length - 1)
        {
            currentCircleIndex++;
            GameObject nextCircle = circles[currentCircleIndex];
            nextCircle.SetActive(true);
            StartCoroutine(FadeIn(nextCircle));
        }
    }

    private IEnumerator FadeIn(GameObject circle)
    {
        CanvasGroup canvasGroup = circle.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = circle.AddComponent<CanvasGroup>();
        }

        canvasGroup.alpha = 0f; // Start with invisible
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 1f; // Ensure the final alpha is 1
    }
}
