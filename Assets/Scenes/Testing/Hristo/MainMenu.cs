using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : View
{
    [SerializeField] List<GameObject> menuList;
    List<Button> buttons;
    /**
     Loads scene given sceneName string parameter.
     Use on Button OnClick method in Editor.
     Add the name of the scene i.e. "Garage" as parameter here.
     **/
    //public void OnButtonPressLoadScene(string sceneName) => SceneManager.LoadScene(sceneName);
    /***
    Enables selected menu given as string parameter, disables all else.
    Use on Button OnClick Function in Editor.
    Add the name of the menu i.e. "Level Select" as parameter.
    ***/
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

    }
    public void OnButtonPressLoadMenu(string menuName)
    {
        //set inactive for all other sub-menus
        menuList.Where(n => n.name != menuName).ToList().ForEach(n => n.SetActive(false));
        //set active for selected sub-menu
        GameObject activeMenu = menuList.Find(x => x.name == menuName);
        activeMenu.SetActive(true);
    }

    public void ChangeMasterVol()
    {
        //mixer.setVol(Mathf.Log10(slider)*20);
    }

    public void StartGame()
    {
        GameManager.Instance.StartGame();
        CloseView();
    }
}
