using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        pause,
        running,
        over,
    }

    private static GameManager _instance;
    private GameState _state;
    public static GameManager Instance { get { return _instance; } }
    public static GameState State
    {
        get { return _instance._state; }
    }

    public static event Action OnGameOver;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    

    private void Start()
    {
        TimerManager.OnTimerEnd -= GameOver;
        TimerManager.OnTimerEnd += GameOver;

        // Initialize the game
        InitializeGame();
    }
    
    private void InitializeGame()
    {
        // Set time scale to 0, to not run anything before game start
        Time.timeScale = 0;
        
        // Load the first level
        LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
        UIManager.Instance.ActivateView("MainMenuView");
    }
    
    private void LoadLevel(int levelIndex)
    {
        // Load the specified level
        SceneManager.LoadScene(levelIndex);
    }

    public void StartGame()
    {
        _state = GameState.running;
        Time.timeScale = 1;
        TimerManager.Instance.StartTimer();
        // Start level music
        SoundManager.Instance.PlayTheme();
        ScoreManager.Instance.ResetScore();
    }

    public void PauseGame()
    {
        _state = GameState.pause;
        Time.timeScale = 0;
        TimerManager.Instance.PauseTimer();
        // Stop music
        SoundManager.Instance.PauseMusicAndSfx();
    }

    public void ResumeGame()
    {
        _state = GameState.running;
        Time.timeScale = 1;
        TimerManager.Instance.StartTimer();
        SoundManager.Instance.UnPauseMusicAndSfx();
    }

    /// <summary>
    /// Restarts level from scratch
    /// </summary>
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        StartGame();
    }

    private void Update()
    {
        // Pause menu check
        if (_state == GameState.running && Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
            UIManager.Instance.ActivateView("PauseMenuView");
        }
        
        // Check for game over condition
        if (_state != GameState.over)
        {
            // Check other game conditions, handle player input, etc.
        }
        else
        {
            // Handle game over state, show game over screen, restart, etc
        }
    }

    public void GameOver()
    {
        _state = GameState.over;
        
        // Stop the scene
        Time.timeScale = 0;
        
        // Stop music
        SoundManager.Instance.StopMusicAndSfx();
        
        // Run listeners
        OnGameOver?.Invoke();

        // Show Game Over screen
        UIManager.Instance.ActivateView("GameOverView");
    }
}
