using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class PickupBall : MonoBehaviour
{

    [Header("Pickup Settings")]
    [SerializeField] Transform holdArea;
    GameObject heldObj;
    Rigidbody heldObjRB;

    [Header("Physics Parameters")]
    [SerializeField] float pickupRange = 5.0f;
    [SerializeField] float pickupForce = 150.0f;

    public MyInputs playerControls;
    InputAction click;
    InputAction restartLevel;


    private void Awake() 
    {
        playerControls = new MyInputs();   
    }
    private void OnEnable() 
    {
       click = playerControls.Player.Fire;
       click.Enable();
       click.performed += Click;

       restartLevel = playerControls.Player.Restart;
       restartLevel.Enable();
       restartLevel.performed += RestartLevel;
    }

    private void OnDisable() 
    {
        click.Disable();   
        restartLevel.Disable();
    }

    void Start()
    {
        
    }

    void Update()
    {
        //Picking up objects, maybe change back to original method with hinge joint
        //https://www.youtube.com/watch?v=6bFCQqabfzo&ab_channel=SpeedTutor
      //  bool clicked = Mouse.current.leftButton.wasPressedThisFrame;
        // bool clicked = click.ReadValue<>();
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward *pickupRange), Color.cyan);
        Debug.DrawRay(transform.position+ new Vector3(-0.25f, 0, 0), transform.TransformDirection(Vector3.forward *pickupRange), Color.red);
        Debug.DrawRay(transform.position+ new Vector3(0.25f, 0, 0), transform.TransformDirection(Vector3.forward *pickupRange), Color.green);

        if (heldObj == null)
        {
            RaycastHit hit;
            float rayOffset = 0.25f;
            for (int i = -1; i < 2; i++)
            {
                if(Physics.Raycast(transform.position + new Vector3(rayOffset * i, 0, 0), transform.TransformDirection(Vector3.forward), out hit, pickupRange, LayerMask.GetMask("Ball")))
                {
                    //Highlight
                    Debug.Log(hit.transform.name);
                    hit.collider.GetComponent<HighLightBall>().SetLit();
                    break;
                }
            }
        }
    }

    private void FixedUpdate() 
    {
        if(heldObj != null)
        {
            //move it
            MoveObject();
        }    
    }

    void MoveObject()
    {
        if(Vector3.Distance(heldObj.transform.position, holdArea.position) > 0.01f)
        {
            Vector3 moveDirection = (holdArea.position - heldObj.transform.position);
            heldObjRB.AddForce(moveDirection * pickupForce);
        }
    }

    void PickupObject(GameObject pickObj)
    {
        if(pickObj.GetComponent<Rigidbody>() )
        {
            heldObjRB = pickObj.GetComponent<Rigidbody>();
            //heldObjRB.useGravity = false;
            heldObjRB.drag = 10;
            heldObjRB.constraints = RigidbodyConstraints.FreezePositionY;

            heldObjRB.transform.parent = holdArea;
            heldObj = pickObj;
        }
    }

    public void DropObject()
    {
        if(heldObj != null)
        {
            heldObjRB.useGravity = true;
            heldObjRB.drag = 1;
            heldObjRB.constraints = RigidbodyConstraints.None;

            heldObjRB.transform.parent = null;
            heldObj = null;
        }
     
    }
    void Click(InputAction.CallbackContext context)
    {
         if(heldObj == null)
            {
                RaycastHit hit;

                float rayOffset = 0.25f;
                for (int i = -1; i < 2; i++)
                {
                    if(Physics.Raycast(transform.position + new Vector3(rayOffset * i, 0, 0), transform.TransformDirection(Vector3.forward), out hit, pickupRange, LayerMask.GetMask("Ball")))
                    {
                        //pickup
                        Debug.Log(hit.transform.name);
                        PickupObject(hit.transform.gameObject);
                        break;
                    }
                }
            }
            else
            {
                //drop
                DropObject();
            }
    }

    void RestartLevel( InputAction.CallbackContext context)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
