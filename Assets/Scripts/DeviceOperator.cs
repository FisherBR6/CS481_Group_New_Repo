using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DeviceOperator : MonoBehaviour
{
    public Camera playerCamera;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            CheckHit(ray);
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = playerCamera.ScreenPointToRay(Input.GetTouch(0).position);
            CheckHit(ray);
        }
    }

    void CheckHit(Ray ray)
    {
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Renderer rend = hit.collider.GetComponent<Renderer>();
            if (rend != null)
            {
                hit.collider.GetComponent<Renderer>().material.color = Random.ColorHSV();
            }
        }
    }
}
