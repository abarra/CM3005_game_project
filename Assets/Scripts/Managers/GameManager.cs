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

    public static event Action OnGameOver;

    public int score {get; private set;}

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
        TimerManager.Instance.StartTimer();
        // Start level music
        SoundManager.Instance.PlayTheme();
        score = 0;

    }

    public void PauseGame()
    {
        _state = GameState.pause;
        TimerManager.Instance.PauseTimer();

    }

    private void Update()
    {
        // Check for game over condition
        if (_state != GameState.over)
        {
            // Check other game conditions, handle player input, etc.
        }
        else
        {
            // Handle game over state, show game over screen, restart, etc.
            UIManager.Instance.ActivateView("MainMenuView");

        }
    }

    public void GameOver()
    {
        _state = GameState.over;
        
        OnGameOver?.Invoke();

        //TODO show game over overlay
        Debug.Log("GameOver");

    }

    public void UpdateScore(int value)
    {
        score += value;
        GetComponentsInChildren<TextMeshProUGUI>().Where(n => n.gameObject.name == "ScoreText").ToArray()[0].text = "Score: " + score.ToString();
    }
}
