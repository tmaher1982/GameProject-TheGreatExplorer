using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraPosition : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float rotationSpeed = 0.25f;
    Vector3 offset = Vector3.zero;
    public MyInputs playerControls;
    InputAction look;
    float rotationAngle = 0.0f;

    private void Awake() 
    {
        playerControls = new MyInputs();   
    }
    private void OnEnable() 
    {
        // playerControls.Enable();
        look = playerControls.Player.Look;
        look.Enable();
    }

    private void OnDisable() 
    {
        look.Disable();   
    }
    // Start is called before the first frame update
    void Start()
    {
       
        // offset = transform.position - player.position;
        transform.position = player.position;
    }

    // Update is called once per frame
    void Update()
    {
         Vector3 rot = look.ReadValue<Vector2>();
         transform.Rotate(transform.rotation.x, rot.x* rotationSpeed , transform.rotation.z );
       // transform.Rotate(rot.y*rotationSpeed, transform.rotation.y , -rot.y*rotationSpeed );
    //    transform.rotation = Quaternion.Lerp( transform.rotation,Quaternion.Euler(transform.rotation.x, rot.x , transform.rotation.z), Time.deltaTime );
        transform.position = Vector3.Lerp(transform.position, player.position + offset, 10.0f * Time.deltaTime);
        
    }
}
