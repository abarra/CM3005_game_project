using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinView : MenuView
{
    public void NextLevel()
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