using UnityEngine;

public class SkyboxAnimator : MonoBehaviour
{
    public float rotationSpeed = 1.0f;

    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotationSpeed);
    }
}
