using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Pause,
        Running,
        Over,
        Win,
    }

    private GameState state;
    public static GameManager Instance { get; private set; }

    public static GameState State => Instance.state;

    public static event Action OnGameOver;
    public static event Action OnGameWin;

    private void Awake()
    {
        // Singleton functionality and do not destroy it instruction
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
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

    /// <summary>
    /// Start game with all depends modules
    /// </summary>
    public void StartGame()
    {
        state = GameState.Running;
        Time.timeScale = 1;
        TimerManager.Instance.StartTimer();
        // Start level music
        SoundManager.Instance.PlayTheme();
        // Reset score to 0
        ScoreManager.Instance.ResetScore();

        // emition level
        EmotionController.Instance.Reset();
    }

    /// <summary>
    /// Pause the game
    /// </summary>
    public void PauseGame()
    {
        // Change game state to pause
        state = GameState.Pause;
        // Set timescale to 0 to stop running all time depending functions
        Time.timeScale = 0;
        // Pause game timer
        TimerManager.Instance.PauseTimer();
        // Stop music
        SoundManager.Instance.PauseMusicAndSfx();
    }

    /// <summary>
    /// Resume game from pause
    /// </summary>
    public void ResumeGame()
    {
        // Change game state to running
        state = GameState.Running;
        // Set timescale to 1 to resume all time depends functions
        Time.timeScale = 1;
        // Start timer
        TimerManager.Instance.StartTimer();
        // Resume all music
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
        // Pause menu check. Run pause on P or ESC press
        if (state == GameState.Running && (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)))
        {
            PauseGame();
            UIManager.Instance.ActivateView("PauseMenuView");
        }
        
        // Check for game over condition
        if (state == GameState.Running)
        {
            // Check other game conditions, handle player input, etc.
        }
        else
        {
            // Handle game over state, show game over screen, restart, etc
        }
    }

    /// <summary>
    /// Stops the game level and activate GameOver view
    /// </summary>
    public void GameOver()
    {
        state = GameState.Over;
        
        // Stop the scene
        Time.timeScale = 0;
        
        // Stop music
        SoundManager.Instance.StopMusicAndSfx();
        
        // Run listeners
        OnGameOver?.Invoke();

        // Show Game Over screen
        UIManager.Instance.ActivateView("GameOverView");
    }
    
    /// <summary>
    /// Stops the game level and activates Win view
    /// </summary>
    public void Win()
    {
        state = GameState.Win;
        
        // Stop the scene
        Time.timeScale = 0;
        
        // Stop music
        SoundManager.Instance.StopMusicAndSfx();
        
        // Run listeners
        OnGameWin?.Invoke();

        // Show Game Over screen
        UIManager.Instance.ActivateView("WinView");
    }
}
