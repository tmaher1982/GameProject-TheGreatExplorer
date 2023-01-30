using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighLightBall : MonoBehaviour
{
    public Color highlightColor;
    Material mat;
    public float timeout = 0.25f;
    float timer = 0f;
    void Start()
    {
        mat = this.GetComponent<MeshRenderer>().material;
    }

    void Update()
    {
        if(timer > 0f)
        {
            Color lerpedColor = Color.Lerp(mat.GetColor("_EmissionColor"), highlightColor, 0.1f);
            mat.SetColor("_EmissionColor", lerpedColor );
        }
        else
        {
            Color lerpedColor = Color.Lerp(mat.GetColor("_EmissionColor"), new Vector4(0f,0f,0f,1f), 0.1f);
            mat.SetColor("_EmissionColor", lerpedColor );
        }

        timer -= Time.deltaTime;
    }

    public void SetLit()
    {
        timer = timeout;
    }

   
}
