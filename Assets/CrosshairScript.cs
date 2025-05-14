using UnityEngine;

public class CrosshairScript : MonoBehaviour
{
    private Renderer cubeRenderer;
    MeshRenderer mesh;

    private void Start()
    {
        cubeRenderer = GetComponent<Renderer>();
        mesh = GetComponent<MeshRenderer>();
    }

    public void OnPointerEnter()
    {
        // Optional: highlight on gaze
         mesh.enabled = true;
        cubeRenderer.material.color = Color.yellow;
    }

    public void OnPointerExit()
    {
        // Reset when gaze leaves
        cubeRenderer.material.color = Color.white;
         mesh.enabled = false;
    }

    public void OnPointerClick()
    {
        cubeRenderer.material.color = Color.red;
    }
}