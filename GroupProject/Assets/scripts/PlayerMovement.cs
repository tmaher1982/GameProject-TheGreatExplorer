using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // public InputAction playerControls;
    public MyInputs playerControls;

    Vector2 moveDirection = Vector2.zero;

    public float movementSpeed;
    public float rotationSpeed;

    Rigidbody rb;
    

    InputAction move;

    private void Awake() 
    {
        playerControls = new MyInputs();   
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
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        
        // var movement = Input.GetAxis(("Vertical"));
        // var turn = Input.GetAxis(("Horizontal"));
         moveDirection = move.ReadValue<Vector2>();
      //  horizontal = playerControls.ReadValue<;

        //this works too
        // rb.velocity = moveDirection.y* transform.forward* movementSpeed;
        // rb.angularVelocity = new Vector3(0, moveDirection.x, 0) * rotationSpeed;

        // rb.AddForce(moveDirection.y* transform.forward* movementSpeed);
        // rb.AddTorque( new Vector3(0, moveDirection.x, 0) * rotationSpeed);

        //this works
        // transform.Rotate(rotationSpeed * Time.deltaTime * new Vector3(0, moveDirection.x, 0) );
        // transform.Translate( movementSpeed * Time.deltaTime * new Vector3(0,0,moveDirection.y));
        
    }

    private void FixedUpdate() 
    {
        rb.velocity = moveDirection.y* transform.forward* movementSpeed;
        rb.angularVelocity = new Vector3(0, moveDirection.x, 0) * rotationSpeed;
        
    }

    private void OnTriggerEnter(Collider other) 
    {
       // if(other.tag == "Enemy")
        // {
        //    Debug.Log("Dead Meat", other);
        //   //  other.transform.parent = this.transform;
        // }
        
    }
}
