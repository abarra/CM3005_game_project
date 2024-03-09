using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuView : MenuView
{
    protected override void Update()
    {
        base.Update();
        // Resume game on ESC or P
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            ResumeGame();
        }
    }

    /// <summary>
    /// Resume current level
    /// </summary>
    public void ResumeGame()
    {
        GameManager.Instance.ResumeGame();
        CloseView();
    }
}