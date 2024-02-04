using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }
    
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
}
