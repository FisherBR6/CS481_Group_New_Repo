using UnityEngine;
using UnityEngine.UI;

public class TapSelector : MonoBehaviour
{
    public float maxDistance = 1000f;
    public Image reticle;

    private GameObject currentButton;

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        //Making sure ray is pointing at a button
        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            GameObject hitObject = hit.collider.gameObject;

            if (hitObject.CompareTag("KeyButton"))
            {
                if (currentButton != hitObject)
                {
                    if (currentButton != null)
                        ResetButtonColor(currentButton);

                    currentButton = hitObject;
                    HighlightButton(currentButton);
                }

                if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0))
                    Debug.Log(currentButton.name);
            }
            else
            {
                if (currentButton != null)
                    ResetButtonColor(currentButton);
                currentButton = null;
            }
        }else
        {
            if (currentButton != null)
                ResetButtonColor(currentButton);
            currentButton= null;
        }
    }

    void HighlightButton(GameObject button)
    {
        var renderer = button.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = Color.green;
        }
    }

    void ResetButtonColor(GameObject button)
    {
        var renderer = button.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = Color.white;
        }
    }
}
