using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class Transition_DonutWipe : MonoBehaviour
{

    // https://forum.unity.com/threads/cant-access-material-on-canvas-image.957416/
    [SerializeField] public Color Color = Color.black;
    [SerializeField][Range(0.0f,2.0f)] public float WipeAmount = 0f;
    [SerializeField][Range(0.0f,1.0f)] public float Sharpness = 0.01f;
    [SerializeField] public bool Reverse = false;
    public Image image;
    private Color ColorActualValue;
    private float WipeAmountActualValue;
    private float SharpnessActualValue;
    private float ReverseActualValue;
 
    private void Awake() 
    {   
        image = GetComponent<Image>();
        // image = this.GetComponent<Image>();
        ColorActualValue = image.material.GetColor("_Color");
        WipeAmountActualValue = image.material.GetFloat("_WipeAmount");
        SharpnessActualValue = image.material.GetFloat("_Sharpness");
        if(image.material.GetFloat("_Reverse") == 0)
        {
            ReverseActualValue = 0;
        }
        else
            ReverseActualValue = 1;
    }
 
    private void LateUpdate()
    {
        if(WipeAmount != WipeAmountActualValue) 
        {
            WipeAmountActualValue = WipeAmount;
            image.material.SetFloat("_WipeAmount", WipeAmountActualValue)  ;
        }
        
        if(Color != ColorActualValue) 
        {
            ColorActualValue = Color;
            image.material.SetColor("_Color", ColorActualValue)  ;
        }

        if(Sharpness != SharpnessActualValue) 
        {
            SharpnessActualValue = Sharpness;
            image.material.SetFloat("_Sharpness", SharpnessActualValue)  ;
        }

        if(Reverse != FloatToBool(ReverseActualValue)) 
        {
            ReverseActualValue = BoolToFloat(Reverse);
            image.material.SetFloat("_Reverse", ReverseActualValue)  ;
        }

    }
    private bool FloatToBool(float val)
    {
        if(val == 0)
            return false;
        return true;
    }

    private float BoolToFloat(bool val)
    {
        if(!val)
            return 0;
        return 1;
    }
}
