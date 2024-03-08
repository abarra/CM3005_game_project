using System.Globalization;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager _instance;
    public static ScoreManager Instance { get { return _instance; } }
    
    public TextMeshProUGUI scoreText;
    public Animator animator;
    private static readonly int Bounce = Animator.StringToHash("Bounce");

    public int Score {get; private set;}

    public string FormattedScore
    {
        get
        {
            var nfi = new CultureInfo( "en-US", false ).NumberFormat;
            nfi.NumberDecimalDigits = 0;
            return Score.ToString("N", nfi);
        }
    }
    
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
        if (scoreText != null)
        {
            scoreText.text = FormattedScore;
        }

        if (animator != null)
        {
            animator.SetTrigger(Bounce);
        }
    }

}