using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuView : View
{
    public void ResumeGame()
    {
        GameManager.Instance.StartGame();
        CloseView();
    }
}
