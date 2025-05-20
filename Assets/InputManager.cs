using System.Collections;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Camera mainCamera;
    public float dwellTime = 2f; // seconds to trigger a dwell
    public bool useDwell = false;

    private GameObject currentTarget = null;
    private float gazeTimer = 0f;

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        Ray ray;

        // Determine platform-specific ray
        if (Application.isEditor || Application.platform == RuntimePlatform.WindowsPlayer)
        {
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        }
        else
        {
            var reticlePointer = mainCamera.GetComponentInChildren<CardboardReticlePointer>();
            if (reticlePointer == null) return;
            ray = new Ray(reticlePointer.transform.position, reticlePointer.transform.forward);
        }

        HandleRaycast(ray);

        if (!useDwell && Google.XR.Cardboard.Api.IsTriggerPressed && currentTarget != null)
        {
            TriggerKey(currentTarget);
        }
    }

    void HandleRaycast(Ray ray)
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObj = hit.collider.gameObject;

            if (hitObj.CompareTag("KeyButton"))
            {
                if (hitObj != currentTarget)
                {
                    currentTarget = hitObj;
                    gazeTimer = 0f;
                }
                else if (useDwell)
                {
                    gazeTimer += Time.deltaTime;
                    if (gazeTimer >= dwellTime)
                    {
                        TriggerKey(currentTarget);
                        gazeTimer = 0f; // prevent repeat triggering
                    }
                }
            }
            else
            {
                ResetGaze();
            }
        }
        else
        {
            ResetGaze();
        }
    }

    void ResetGaze()
    {
        currentTarget = null;
        gazeTimer = 0f;
    }

    void TriggerKey(GameObject keyObj)
    {
        Debug.Log($"Key triggered: {keyObj.name}");
        keyObj.SendMessage("OnKeyPress", SendMessageOptions.DontRequireReceiver);
    }

    
    public void ToggleInputMode()
    {
        useDwell = !useDwell;
        Debug.Log("Input mode changed: " + (useDwell ? "Dwell" : "Click"));
    }
}

