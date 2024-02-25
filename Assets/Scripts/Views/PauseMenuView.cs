using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuView : MenuView
{
    public void ResumeGame()
    {
        GameManager.Instance.StartGame();
        CloseView();
    }

    public void RestartGame()
    {
        
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