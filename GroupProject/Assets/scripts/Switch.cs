using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField] GameObject activationObject;
    ParticleSystem ringEffect;
    bool activated = false;
    void Start()
    {
        // Particle effect that plays when the ball is on the switch
        ringEffect = GetComponent<ParticleSystem>();
        ringEffect.Stop();
    }
   
    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject == activationObject)    
        {
            activated = true;
            ringEffect.Play();
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.gameObject == activationObject)    
        {
            activated = true;
            ringEffect.Stop();
        }    
    }
}
