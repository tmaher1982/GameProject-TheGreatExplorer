using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScreen : MonoBehaviour
{
    public Animator fadeIn;
    // Start is called before the first frame update
    void Start()
    {
        fadeIn.SetBool("Start", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
