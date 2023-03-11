using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    // How to make AWESOME Scene Transitions in Unity!
    // https://www.youtube.com/watch?v=CE9VOZivb3I
    public Animator transition;
    // This is the time after the transition before the next scene is loaded
    public float transitionTime = 1f;
  
    public void StartGame()
    {
        StartCoroutine(LoadScene());
    }
    public void TitleScreen()
    {
        StartCoroutine(LoadTitleScreen());
    }

    public void NextLevel()
    {
        StartCoroutine(LoadNextLevel());
    }

    public void QuitToTitle()
    {
        StartCoroutine(ReloadTitleScreen());
    }

    IEnumerator LoadScene()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene("Tutorial2");
    }
    IEnumerator LoadTitleScreen()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        Destroy(GameManager.instance);
        SceneManager.LoadScene("Title");
    }
    IEnumerator LoadNextLevel()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        Destroy(GameManager.instance);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator ReloadTitleScreen()
    {
        Debug.Log("Quit to title");
        if (Time.timeScale == 0)
            Time.timeScale = 1;
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(0.1f);
        Destroy(GameManager.instance);
        SceneManager.LoadScene("Title");
    }
}
