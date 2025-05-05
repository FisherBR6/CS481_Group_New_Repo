using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class UIRaycaster : MonoBehaviour
{
    public Camera mainCam;
    private GraphicRaycaster raycaster; // Drag your Canvas's GraphicRaycaster here

    private GameObject currentButton;

    void Update()
    {
        // Gaze center point (can use mouse position for PC testing)
        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);

#if UNITY_EDITOR
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        pointerData.position = Input.mousePosition; // Mouse-based input
#else
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        pointerData.position = screenCenter; // Gaze-based input
#endif

        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(pointerData, results);

        if (results.Count > 0)
        {
            GameObject hitObject = results[0].gameObject;

            if (hitObject.CompareTag("KeyButton"))
            {
                if (currentButton != hitObject)
                {
                    if (currentButton != null) ResetButtonColor(currentButton);
                    currentButton = hitObject;
                    HighlightButton(currentButton);
                }

                if (Input.GetMouseButtonDown(0)) // Works on PC
                {
                    Debug.Log("Clicked: " + currentButton.name);
                }
            }
        }
        else
        {
            if (currentButton != null)
            {
                ResetButtonColor(currentButton);
                currentButton = null;
            }
        }
    }

    void HighlightButton(GameObject button)
    {
        var image = button.GetComponent<Image>();
        if (image != null)
            image.color = Color.green;
    }

    void ResetButtonColor(GameObject button)
    {
        var image = button.GetComponent<Image>();
        if (image != null)
            image.color = Color.white;
    }
}
