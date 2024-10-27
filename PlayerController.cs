using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform nextHalo;  // Reference to the next halo to fade in

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Halo"))
        {
            FadeHalo fadeHalo = nextHalo.GetComponent<FadeHalo>();
            if (fadeHalo != null)
            {
                fadeHalo.FadeIn();
            }

            // Optionally, set the next halo for the subsequent transition
            nextHalo = GetNextHalo();
        }
    }

    private Transform GetNextHalo()
    {
        // Implement your logic to determine the next halo
        // For example, you could use an array or list of halos and return the next one
        return null;  // Placeholder
    }
}
