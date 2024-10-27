using UnityEngine;

public class AdjustCollider : MonoBehaviour
{
    public bool is2D = true; // Set to false if working in 3D

    void Start()
    {
        if (is2D)
        {
            CircleCollider2D circleCollider = gameObject.GetComponent<CircleCollider2D>();
            if (circleCollider == null)
            {
                circleCollider = gameObject.AddComponent<CircleCollider2D>();
            }

            float radius = GetComponent<SpriteRenderer>().bounds.size.x / 2;
            circleCollider.radius = radius;
        }
        else
        {
            SphereCollider sphereCollider = gameObject.GetComponent<SphereCollider>();
            if (sphereCollider == null)
            {
                sphereCollider = gameObject.AddComponent<SphereCollider>();
            }

            float radius = GetComponent<Renderer>().bounds.size.x / 2;
            sphereCollider.radius = radius;
        }
    }
}
