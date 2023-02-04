using System.Collections;
using System.Collections.Generic;
// using System;
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
        DontDestroyOnLoad(gameObject);

        levelTimer = 50;
        playerLives = 3;
        UpdateGameState(GameState.InGame);

        playerControls = new MyInputs();   
      
    }

    public void UpdateGameState(GameState newState)
    {
        state = newState;

        switch (state)
        {
            case GameState.PlayerDead:
                onPlayerDied.Invoke(); 
                playerLives -= 1;

                playerIsAlive = false;
                
                if(playerLives < 0 )
                {
                    UpdateGameState(GameState.GameOver);
                    return;
                }
                StartCoroutine(WaitAfterDeath());
                break;

            case GameState.Paused:
                // Freeze game
                Time.timeScale = 0;
                break;

            default:
                break;
        }

        OnGameStateChanged.Invoke();

    }
    // Start is called before the first frame update
    void Start()
    {
      
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case GameState.InGame:
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
                OnGameOver.Invoke();
                break;

            case GameState.Paused:
                UpdateGameState(GameState.Paused);
                break;


            default:
                break;
        }
       
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
    }

    private void OnDisable() 
    {
        restartLevel.Disable();
    }
    // This should be somewhere else
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
    TransitionOut
}