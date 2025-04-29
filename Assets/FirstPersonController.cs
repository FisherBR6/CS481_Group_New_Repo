using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float sensitivity = 3f;
    public float movementSpeed = 15f;
    public Transform player;

    private float rotationX = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        //Rotating up/down
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
        transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        player.Rotate(Vector3.up * mouseX);
        //Rotate player body based on mouse x
        //transform.Rotate(Vector3.up * mouseX);
    }
}
