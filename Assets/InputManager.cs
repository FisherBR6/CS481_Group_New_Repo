using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    public Camera mainCamera;
    private GraphicRaycaster graphicRaycaster;  // The raycaster for UI
    private Canvas canvas; 

    private void Start()
    {
        FindCanvasAfterSceneLoad();
    }

    private void Update()
    {
        // Check if it's a computer and use mouse input
        if (Application.isEditor || Application.platform == RuntimePlatform.WindowsPlayer)
        {
            Vector2 mousePosition = Input.mousePosition;

            Ray ray = mainCamera.ScreenPointToRay(mousePosition);
            HandleRaycast(ray);
        }
        // Mobile devices will use the CardboardReticlePointer for raycasting
        else if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            CardboardReticlePointer reticlePointer = mainCamera.GetComponentInChildren<CardboardReticlePointer>();
            if (reticlePointer != null)
            {
                Ray ray = new Ray(reticlePointer.transform.position, reticlePointer.transform.forward);
                HandleRaycast(ray);
            }
        }
    }

    void HandleRaycast(Ray ray)
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.collider.gameObject;

            if (hitObject.CompareTag("KeyButton"))
            {
                Debug.Log($"Key pressed: {hitObject.name}");
            }
        }
    }

    private IEnumerator FindCanvasAfterSceneLoad()
    {
        yield return new WaitForSeconds(0.5f);
        // Check if the scene is loaded and active
        Scene qwertyScene = SceneManager.GetSceneByName("QWERTY");
        if (qwertyScene.isLoaded)
        {
            Debug.Log("QWERTY scene is loaded.");
            canvas = FindObjectOfType<Canvas>();
            if (canvas != null)
            {
                graphicRaycaster = canvas.GetComponent<GraphicRaycaster>();
                Debug.Log("Canvas found and GraphicRaycaster attached.");
            }
            else
            {
                Debug.LogError("No Canvas found in the loaded scene.");
            }
        }
        else
        {
            Debug.LogError("QWERTY scene is not loaded yet.");
        }
    }
}
