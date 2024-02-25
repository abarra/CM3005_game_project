using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager _instance;
    public static ScoreManager Instance { get { return _instance; } }
    
    public TextMeshProUGUI scoreText;
    
    public int Score {get; private set;}
    
    private void Awake()
    {
        // Singleton pattern implementation
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

    public void UpdateScore(int value)
    {
        Score += value;
        UpdateText();
    }

    public void ResetScore()
    {
        Score = 0;
        UpdateText();
    }

    private void UpdateText()
    {
        scoreText.text = Score.ToString();
    }

}