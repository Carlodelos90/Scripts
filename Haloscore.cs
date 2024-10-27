using UnityEngine;

public class HaloScore : MonoBehaviour
{
    public int maxScore = 100; // Maximum score for hitting the center
    public int scoreThreshold = 90; // Score threshold to avoid losing a life
    public AnimationCurve scoreCurve = AnimationCurve.EaseInOut(0, 1, 1, 0); // Score curve for adjusting difficulty
    private BoxCollider boxCollider;
    public DynamicScoreText dynamicScoreText; // Reference to DynamicScoreText

    private LifeManager lifeManager; // Reference to LifeManager

    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        if (boxCollider == null)
        {
            Debug.LogError("Missing BoxCollider component on the HaloScore GameObject.");
        }

        // Find the DynamicScoreText in the scene
        if (dynamicScoreText == null)
        {
            dynamicScoreText = FindObjectOfType<DynamicScoreText>();
            if (dynamicScoreText == null)
            {
                Debug.LogError("DynamicScoreText script not found in the scene.");
            }
        }

        // Find the LifeManager in the scene
        lifeManager = FindObjectOfType<LifeManager>();
        if (lifeManager == null)
        {
            Debug.LogError("LifeManager script not found in the scene.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter with: " + other.gameObject.name);

        if (other.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Calculate the distance from the center of the box collider
                Vector3 boxCenter = transform.TransformPoint(boxCollider.center);
                Vector3 playerPosition = other.transform.position;
                float distance = Vector3.Distance(playerPosition, boxCenter);

                Debug.Log("Player position: " + playerPosition);
                Debug.Log("Box collider center: " + boxCenter);
                Debug.Log("Distance to center: " + distance);

                // Adjust the max distance based on the size of the box collider
                float maxDistance = boxCollider.size.x / 2;

                // Normalize the distance to a value between 0 and 1
                float normalizedDistance = Mathf.Clamp01(distance / maxDistance);

                // Calculate score using the score curve
                float curveValue = scoreCurve.Evaluate(normalizedDistance);
                int score = Mathf.RoundToInt(curveValue * maxScore);
                score = Mathf.Max(score, 0); // Ensure the score is not negative
                Debug.Log("Calculated score: " + score);

                // Add score to the ScoreManager
                ScoreManager.instance.AddScore(score);

                // Show the dynamic score
                if (dynamicScoreText != null)
                {
                    dynamicScoreText.ShowDynamicScore(score);
                }

                // Check if the score is below the threshold and reduce life if necessary
                if (score < scoreThreshold && lifeManager != null)
                {
                    lifeManager.LoseLife();
                }

                // Disable the current circle to prevent re-triggering
                gameObject.SetActive(false);
            }
        }
    }
}
