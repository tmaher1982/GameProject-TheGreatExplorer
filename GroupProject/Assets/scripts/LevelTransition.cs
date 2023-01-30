using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    // How to make AWESOME Scene Transitions in Unity!
    // https://www.youtube.com/watch?v=CE9VOZivb3I
    public Animator transition;
    public float transitionTime = 1f;
    public void StartGame()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene("Example_Scene");
    }
}
