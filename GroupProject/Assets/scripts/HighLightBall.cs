using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighLightBall : MonoBehaviour
{
    public bool isLit = false;
    public Color highlightColor;
    Material mat;
    public float timeout = 0.25f;
    float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        mat = this.GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 0f)
        {
            // mat.SetColor("_EmissionColor", highlightColor);
            // mat.GetColor("_EmissionColor")
            // Debug.Log(lerpTimer);
            Color lerpedColor = Color.Lerp(mat.GetColor("_EmissionColor"), highlightColor, 0.1f);
            mat.SetColor("_EmissionColor", lerpedColor );
        }
        else
        {
            // mat.SetColor("_EmissionColor", new Vector4(0f,0f,0f,1f));
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
