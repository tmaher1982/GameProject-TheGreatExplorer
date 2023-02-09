using System.Collections;
using System.Collections.Generic;
//using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;


public class GameManager : MonoBehaviour
{
    // Game Manager Tutorial
    // https://www.youtube.com/watch?v=4I0vonyqMi8
    // Unity Events Tutorial
    // https://www.youtube.com/watch?v=ax1DiSutEy8
    public static GameManager instance;
    public GameState state;
    public UnityEvent OnGameStateChanged;
    public UnityEvent OnGameOver;
    public UnityEvent OnLevelComplete;
    public UnityEvent onPlayerDied;

    //Game paused event (To display menu for example)
    public UnityEvent onGamePaused;

    public Transform playerSpawnPoint;
    public MyInputs playerControls;
    InputAction restartLevel;


    public int playerLives;
    public float levelTimer;
    public bool playerHasObject = false;
    public bool playerIsAlive = true;
    void Awake() 
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        // DontDestroyOnLoad(instance);
        levelTimer = 150;
        playerLives = 3;

        playerControls = new MyInputs();   
        UpdateGameState(GameState.InGame);
        OnGameStateChanged.Invoke();

    }

    void Start()
    {
    }

    public void UpdateGameState(GameState newState)
    {
        state = newState;

        switch (state)
        {
            case GameState.PlayerDead:
                onPlayerDied?.Invoke(); 
                playerLives -= 1;

                playerIsAlive = false;
                
                if(playerLives < 0 )
                {
                    UpdateGameState(GameState.GameOver);
                    return;
                }
                StartCoroutine(WaitAfterDeath());
                break;
            
            case GameState.LevelCompleted:
                OnLevelComplete?.Invoke();
                break;
            
            case GameState.InGame:
                
                break;

            case GameState.LevelStart:
                

            case GameState.Paused:
                // Freeze game
                Time.timeScale = 0;
                break;

            default:
                break;
        }

        OnGameStateChanged?.Invoke();

    }
 

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case GameState.InGame:
                levelTimer -= Time.deltaTime;
                // Check if game is frozen
                if (Time.timeScale == 0)
                    Time.timeScale = 1;

                levelTimer-=Time.deltaTime;

                if(levelTimer < 0.1)
                {
                    UpdateGameState(GameState.GameOver);
                }
                break;

            case GameState.PlayerDead:
                break;

            case GameState.GameOver:
                OnGameOver?.Invoke();
                break;

            case GameState.Paused:
                UpdateGameState(GameState.Paused);
                break;


            default:
                break;
        }
       
    }

    public void SetGameStateToTransitionOut()
    {
        /*if you want to provide a function to an event in the inspector, the function must meet the following requirements:
        1. The function must be public
        2. The function must have a return type of void
        3. The function must take no or one parameter
        4. If the function takes one parameter, the latter must be one of the following types:
                        int
                        float
                        string
                        bool*/

        UpdateGameState(GameState.TransitionOut);
    }
    public void refreshgamestate()
    {
        // OnGameStateChanged.Invoke();
        // UpdateGameState(GameState.InGame);
        Debug.Log(state);

    }
    IEnumerator WaitAfterDeath()
    {
        yield return new WaitForSeconds(3);
        UpdateGameState(GameState.InGame);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerIsAlive = true;
        player.transform.position = playerSpawnPoint.transform.position;
        player.transform.rotation = Quaternion.Euler(0f, 0f,0f);
    }

    private void OnEnable() 
    {

        restartLevel = playerControls.Player.Restart;
        restartLevel.Enable();
        restartLevel.performed += RestartLevel;

        // Debug
        Debug.Log("GAME MANAGER OnENable");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable() 
    {
        restartLevel.Disable();
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
         Debug.Log("OnSceneLoaded: " + scene.name);
    }
    // This 
    void RestartLevel( InputAction.CallbackContext context)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

public enum GameState
{
    LevelStart,
    InGame,
    Paused,
    PlayerDead,
    GameOver, 
    LevelCompleted, 
    TransitionIn,
    TransitionOut,
    GamePaused
}