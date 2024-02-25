using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuView : MenuView
{
    public void StartGame()
    {
        GameManager.Instance.StartGame();
        CloseView();
    }
}