using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Switch : MonoBehaviour
{
    [SerializeField] GameObject activationObject;
    ParticleSystem ringEffect;
    public bool activated = false;
    LevelData levelData;
    [SerializeField] UnityEvent onSwitchActivated;

    void Start()
    {
        // Particle effect that plays when the ball is on the switch
        ringEffect = GetComponent<ParticleSystem>();
        ringEffect.Stop();

        levelData = FindObjectOfType<LevelData>();
    }
   
    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject == activationObject)    
        {
            // The activationObjects (the balls) have two colliders, we want the one that is a trigger
            if(!other.isTrigger)
            {
                activated = true;
                ringEffect.Play();
                levelData.checkSwitches();
                onSwitchActivated?.Invoke();

            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject == activationObject)
        {
            // Trigger hover movement when the correct ball is on the switch
            other.attachedRigidbody.AddForce(Vector3.up * 6, ForceMode.Acceleration);
            other.attachedRigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX;
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.gameObject == activationObject)    
        {
            // The activationObjects (the balls) have two colliders, we want the one that is a trigger
            if(!other.isTrigger)
            {
                activated = true;
                ringEffect.Stop();
                levelData.checkSwitches();
            }
        }    
    }
}
