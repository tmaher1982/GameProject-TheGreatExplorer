using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{

    AudioSource sounds;
    // Start is called before the first frame update
    void Start()
    {
        sounds = GetComponent<AudioSource>();
    }

    public void PlayerDeathSound()
    {
        sounds.Play();
    }
}
