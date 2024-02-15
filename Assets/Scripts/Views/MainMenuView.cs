using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuView : View
{
    public void StartGame()
    {
        GameManager.Instance.StartGame();
        CloseView();
    }
}
