using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraPosition : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float rotationSpeed = 0.25f;
    public MyInputs playerControls;
    InputAction look;
    float rotationAngle = 0.0f;

    private void Awake() 
    {
        playerControls = new MyInputs();   
    }
    private void OnEnable() 
    {
        look = playerControls.Player.Look;
        look.Enable();
    }

    private void OnDisable() 
    {
        look.Disable();   
    }
    void Start()
    {
        transform.position = player.position;
    }

    void Update()
    {
        // Get Player input
        Vector3 rot = look.ReadValue<Vector2>();
        
        // Rotate around the Y-axis
        transform.Rotate(transform.rotation.x, rot.x* rotationSpeed , transform.rotation.z );
      
        // Follow the player
        transform.position = Vector3.Lerp(transform.position, player.position , 10.0f * Time.deltaTime);
        
    }
}
