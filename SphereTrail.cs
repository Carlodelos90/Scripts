using UnityEngine;

public class SphereTrail : MonoBehaviour
{
    public float radius = 1.0f; // Public variable to set the radius in Inspector
    public float trailSpeed = 5.0f; // Public variable to set the trail speed in Inspector
    public int trailPoints = 100; // Public variable to set the number of trail points in Inspector

    private TrailRenderer trailRenderer;

    void Start()
    {
        trailRenderer = gameObject.GetComponent<TrailRenderer>();
        if (trailRenderer == null)
        {
            trailRenderer = gameObject.AddComponent<TrailRenderer>();
        }

        // Configure the trail renderer
        trailRenderer.time = 2.0f;
        trailRenderer.startWidth = 0.1f;
        trailRenderer.endWidth = 0.1f;
        trailRenderer.material = new Material(Shader.Find("Particles/Standard Unlit"));

        // Initialize the trail
        StartCoroutine(CreateTrail());
    }

    System.Collections.IEnumerator CreateTrail()
    {
        while (true)
        {
            for (int i = 0; i < trailPoints; i++)
            {
                float angle = i * Mathf.PI * 2 / trailPoints;
                Vector3 offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
                trailRenderer.transform.position = transform.position + offset;
                yield return new WaitForSeconds(1.0f / trailSpeed);
            }
        }
    }
}
