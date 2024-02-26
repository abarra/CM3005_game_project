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


    public override void Activate()
    {
        scoreText.text = ScoreManager.Instance.FormattedScore;
    }

    /// <summary>
    /// Close pause menu and open MainMenuView
    /// </summary>
    public void ToMainMenu()
    {
        UIManager.Instance.ActivateView("MainMenuView");
        CloseView();
    }
}