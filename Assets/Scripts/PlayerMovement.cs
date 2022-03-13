using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField] private Transform playerCamera;
    [SerializeField] private CharacterController playerController;

    [SerializeField] private float mouseSensivity = 5f;
    [SerializeField] private float movementSpeed = 4f;
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private float gravity = -9f;

    private Vector3 velocity;
    private Vector3 playerMovementInput;
    private Vector2 playerMouseInput;
    private float camXRotation;

    void Update() {
        
        //Get the inputs from user in every frame
        playerMovementInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        playerMouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        Cursor.visible = false;

        //Call the movement functions in every frame
        Move();
        Look();
    }

    //Movement function
    void Move() {

        //Create move vector from playerMovementInput that taken from update method
        Vector3 moveVector = transform.forward * playerMovementInput.z + transform.right * playerMovementInput.x;

        //If player is in ground, 
        if(playerController.isGrounded) {
            velocity.y = -3f;

            //If user presses space, add velocity.y force to go up
            if (Input.GetKeyDown(KeyCode.Space)) {
                velocity.y = jumpForce;
            }
        //If not, apply gravity
        }else {
            velocity.y -= gravity * -2f * Time.deltaTime;
        }

        //Apply moveVector to the playerController component with delta time
        playerController.Move(moveVector * movementSpeed * Time.deltaTime);
        playerController.Move(velocity * Time.deltaTime);

    }

    //Camera movement function
    void Look() {

        //Take camera x rotation from the user
        camXRotation -= playerMouseInput.y * mouseSensivity;
        //Prevent that user cannot go over -90 or 90 degrees
        camXRotation = Mathf.Clamp(camXRotation, -90, 90);
        transform.Rotate(0f, playerMouseInput.x * mouseSensivity, 0f);
        playerCamera.localRotation = Quaternion.Euler(camXRotation, 0f, 0f);
    }

}
