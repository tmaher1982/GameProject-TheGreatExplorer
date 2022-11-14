using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    public MyInputs playerControls;

    Vector2 moveDirection = Vector2.zero;

    public float movementSpeed;
    public float rotationSpeed;

   // Rigidbody rb;
    InputAction move;
    

    // InputAction move;

    CharacterController controller;
     private Vector3 playerVelocity;
    public bool groundedPlayer;
    private float playerSpeed = 2.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;

    bool alive = true;

    private void Awake() 
    {
        playerControls = new MyInputs();   
        playerSpeed = movementSpeed;
    }
    private void OnEnable() 
    {
        // playerControls.Enable();
        move = playerControls.Player.Move;
        move.Enable();
    }

    private void OnDisable() 
    {
        move.Disable();   
    }

    void Start()
    {
       // rb = GetComponent<Rigidbody>();
       controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // For now just rotate the player when not alive
        if(!alive)
        {
            GetComponent<PickupBall>().DropObject();
            transform.rotation = Quaternion.Euler(90f, 0f,0f);
            return;

        }
        
        // var movement = Input.GetAxis(("Vertical"));
        // var turn = Input.GetAxis(("Horizontal"));
        moveDirection = move.ReadValue<Vector2>();
        // moveDirection.Normalize();
        moveDirection = Vector2.ClampMagnitude(moveDirection, 1f);
        //moveDirection = move
        groundedPlayer = controller.isGrounded;
        // if (groundedPlayer && playerVelocity.y < 0)
        // {
        //     playerVelocity.y = 0f;
        // }

       // Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
       if(groundedPlayer)
            controller.Move(new Vector3(moveDirection.x, 0f, moveDirection.y) * Time.deltaTime * playerSpeed);

        if (moveDirection != Vector2.zero)
        {
            Vector3 lookAt;
            lookAt.x = moveDirection.x;
            lookAt.y = 0f;
            lookAt.z = moveDirection.y;

            Quaternion currentRotation = transform.rotation;
            Quaternion targetRotation = Quaternion.LookRotation(lookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationSpeed *Time.deltaTime);
            //gameObject.transform.forward = new Vector3(moveDirection.x, 0f, moveDirection.y);
        }

        // Changes the height position of the player..
        // if (Input.GetButtonDown("Jump") && groundedPlayer)
        // {
        //     playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        // }

        if(!groundedPlayer)
            playerVelocity.y += gravityValue * Time.deltaTime;// *0.1f;

        controller.Move(playerVelocity * Time.deltaTime);
        
    }

    private void FixedUpdate() 
    {
        //rb.velocity = moveDirection.y* transform.forward* movementSpeed;
        //rb.angularVelocity = new Vector3(0, moveDirection.x, 0) * rotationSpeed;
        
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Enemy")
        {
           Debug.Log("Hit", other);
           alive = false;
        }
        
    }
}
