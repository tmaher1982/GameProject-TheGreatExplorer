using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverSound : MonoBehaviour
{
    AudioSource GameOver;
    
    // Start is called before the first frame update
    void Start()
    {
    
    GameOver = GetComponent<AudioSource>();
    
    }

    public void PlayerDeathSound()
    {
        GameOver.Play();
    }
}
