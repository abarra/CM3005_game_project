using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    string mainGameSceneName = "Main";
    [SerializeField] GameObject buttonList;
    [SerializeField] Button[] buttons;
    void Start()
    {
        buttons = buttonList.GetComponentsInChildren<Button>();
        buttons[0].Select();
    }

    void Update()
    {

    }

    public void LoadMainGame()
    {
        //Debug.Log("loading Main Game");
        SceneManager.LoadScene(mainGameSceneName);
    }
    public void ExitGame() 
    {
        //Debug.Log("quitting Game");
        Application.Quit();
    }
}
