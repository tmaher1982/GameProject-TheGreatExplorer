using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class UpdateLives : MonoBehaviour
{
    private TextMeshProUGUI livesText;

    // Start is called before the first frame update
    void Awake() 
    {
        livesText = GetComponent<TextMeshProUGUI>();  
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    public void updateLives()
    {
        int numLives = GameManager.instance.playerLives;
        livesText = GetComponent<TextMeshProUGUI>();     
       
        if(numLives < 0)
            numLives = 0;
        livesText.text = "Lives<voffset=0.21em>: <voffset=0em>" + numLives.ToString();
    }
}
