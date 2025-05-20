using UnityEngine;

#if UNITY_EDITOR
public class EditorCubeClicker : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left-click
        {
            Ray ray = new Ray(transform.position, transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, 100f))
            {
                //Debug.Log("Editor clicked: " + hit.collider.gameObject.name);
                hit.collider.gameObject.SendMessage("OnKeyPress", SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}
#endif