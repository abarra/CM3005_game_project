using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverView : MenuView
{
    public TextMeshProUGUI scoreText;

    /// <summary>
    /// Runs on Activate to update score
    /// </summary>
    public override void Activate()
    {
        scoreText.text = ScoreManager.Instance.FormattedScore;
    }
}