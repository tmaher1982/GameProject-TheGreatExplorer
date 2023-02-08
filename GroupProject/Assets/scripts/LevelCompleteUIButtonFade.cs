using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompleteUIButtonFade : MonoBehaviour
{
    private CanvasGroup buttonPanel;
    public float delay = 3;
    public bool fadeIn = false;
    public bool fadeOut = false;

    void Start()
    {
        buttonPanel = gameObject.GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        if(fadeIn)
        {
            if(buttonPanel.alpha < 1.0)
            {
                buttonPanel.alpha += Time.deltaTime;
            }
        }        

        if(fadeOut)
        {
            if(buttonPanel.alpha > 0)
            {
                buttonPanel.alpha -= Time.deltaTime;
            }
        }        
    }

    public void FadeIn()
    {
        StartCoroutine(waitFadeIn());
        
    }
    public void FadeOut()
    {
        StartCoroutine(waitFadeOut());
    }

    IEnumerator waitFadeIn()
    {
        yield return new WaitForSeconds(delay);
        fadeOut = false;
        fadeIn = true;
    }
    IEnumerator waitFadeOut()
    {
        yield return new WaitForSeconds(delay);
        fadeIn = false;
        fadeOut = true;
    }
}
