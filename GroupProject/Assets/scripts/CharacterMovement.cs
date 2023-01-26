using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class CharacterMovement : MonoBehaviour
{

    public delegate void PlayerDeath();
    public static event PlayerDeath onPlayerDeath;

   // [SerializeField]
    // UnityEvent onPlayerDied;

    public MyInputs playerControls;

    Vector2 moveDirection = Vector2.zero;
    public Camera mainCam;

    public float movementSpeed = 3.0f;
    public float rotationSpeed;

    InputAction move;

    public Animator animator;

    CharacterController controller;
    public bool groundedPlayer;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;

    bool alive = true;
    public bool invincible = false;

    private void Awake() 
    {
        // Input settings
        playerControls = new MyInputs();   

    }
    private void OnEnable() 
    {
        move = playerControls.Player.Move;
        move.Enable();
    }

    private void OnDisable() 
    {
        move.Disable();   
    }

    void Start()
    {
       controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // For now just rotate the player when not alive
        if(!GameManager.instance.playerIsAlive && !invincible)
        {
            // Drop the ball
            GetComponent<PickupBall>().DropObject();
            // And play dead
            transform.rotation = Quaternion.Euler(90f, 0f,0f);
            animator.SetBool("isRunning", false);
            return;
        }


        if(GameManager.instance.state == GameState.InGame)
        {
            // Get player input        
            moveDirection = move.ReadValue<Vector2>();
            
            // Prevent character from moving faster diagonally (when using keyboard)
            moveDirection = Vector2.ClampMagnitude(moveDirection, 1f);

            // Align character movement with camera rotation
            Vector3 forward = mainCam.transform.forward ;
            Vector3 right = mainCam.transform.right ;
            forward.y = 0f;
            right.y = 0f;
            forward = forward.normalized;
            right = right.normalized;
            Vector3 forwardRelativeVerticalInput = moveDirection.y * forward;
            Vector3 rightRelativeHorizontalInput = moveDirection.x * right;

            Vector3 cameraRelativeMovement = forwardRelativeVerticalInput + rightRelativeHorizontalInput;
        
            // Rotate the character to the direction of the player input accounting for the camera rotation
            if (moveDirection != Vector2.zero)
            {
                Vector3 lookAt = Vector3.zero;;

                lookAt.x = cameraRelativeMovement.x;
                lookAt.z = cameraRelativeMovement.z;

                Quaternion currentRotation = transform.rotation;
                Quaternion targetRotation = Quaternion.LookRotation(lookAt);
                transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationSpeed *Time.deltaTime);
            
                // Player is pressing the move button so play the running animation
                animator.SetBool("isRunning", true);
            }
            else
                animator.SetBool("isRunning", false);


            // If the character is on the ground, it can move. If it is falling we don't want to allow movement
            groundedPlayer = controller.isGrounded;

            if(groundedPlayer)
            {
            controller.Move(cameraRelativeMovement * Time.deltaTime * movementSpeed);
            }
        }

        // The character controller's isGrounded value 'flickers'. Applying 'gravity' keeps it stable and makes the character fall! 
        controller.Move(new Vector3(0, gravityValue, 0) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other) 
    {
        // Check if we've been touched by 'enemies' 
        if(other.tag == "Enemy" && GameManager.instance.playerIsAlive && GameManager.instance.state == GameState.InGame)
        {
            Debug.Log("Hit", other);
           // alive = false;
           GameManager.instance.UpdateGameState(GameState.PlayerDead);
           
        }
    }
}
