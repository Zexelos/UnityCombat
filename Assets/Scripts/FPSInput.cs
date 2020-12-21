using UnityEngine;

public class FPSInput : MonoBehaviour
{
    [SerializeField] CharacterController controller = default;
    [SerializeField] float playerMovementSpeed = 10f;
    [SerializeField] float gravity = -9.81f;

    //Variable for gravity force
    private Vector3 velocity;

    void Update()
    {
        //Get horizontal input
        float inputX = Input.GetAxis("Horizontal");

        //Get vertical input
        float inputY = Input.GetAxis("Vertical");

        //Get the movement direction
        Vector3 move = Vector3.right * inputX + Vector3.forward * inputY;

        //Transform movement direction to local coordinates
        move = transform.TransformDirection(move);

        //Move player
        controller.Move(move * playerMovementSpeed * Time.deltaTime);

        //Set proper gravity value
        velocity.y += gravity * Time.deltaTime;

        //Move player so it is affected by gravity
        controller.Move(velocity * Time.deltaTime);
    }
}