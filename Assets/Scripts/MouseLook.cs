using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] Transform player = default;
    [SerializeField] float sensitivityHor = 100.0f;
    [SerializeField] float sensitivityVert = 100.0f;
    [SerializeField] float minimumVert = -45.0f;
    [SerializeField] float maximumVert = 45.0f;

    //Local variable to keep rotation around x axis
    float rotationX = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //Get horizontal mouse input
        float mouseX = Input.GetAxis("Mouse X") * sensitivityHor;

        //Get vertical mouse input
        float mouseY = Input.GetAxis("Mouse Y") * sensitivityVert;

        //Rotate player object transform around y axis
        player.Rotate(Vector3.up * mouseX);

        //Get vertical mouse input as rotation around x axis
        rotationX -= mouseY;

        //Clamp rotation around x axis so player can not rotate camera in 360 degrees vertically
        rotationX = Mathf.Clamp(rotationX, minimumVert, maximumVert);

        //Change local rotation of x axis of the camera
        transform.localEulerAngles = Vector3.right * rotationX;
    }
}