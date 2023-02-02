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
            }
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
