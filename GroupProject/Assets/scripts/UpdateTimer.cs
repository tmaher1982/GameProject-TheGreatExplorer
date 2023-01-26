using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class UpdateTimer : MonoBehaviour
{
    private TextMeshProUGUI timerText;

    private void Awake() 
    {
        timerText = GetComponent<TextMeshProUGUI>();     
    }
    // Start is called before the first frame update
    void Start()
    {
        timerText.text = "Time: " + Mathf.Floor(GameManager.instance.levelTimer).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.state == GameState.InGame)
        {
            timerText.text = "Time<voffset=0.21em>: <voffset=0em>"+ Mathf.Floor(GameManager.instance.levelTimer).ToString();
        }
    }
  
}
