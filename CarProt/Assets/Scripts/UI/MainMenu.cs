using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    [SerializeField] List<GameObject> menuList;
    void Start()
    {
        OnButtonPressLoadMenu("Main");
    }

    void Update()
    {

    }

    /**
     Loads scene given sceneName string parameter.
     Use on Button OnClick method in Editor.
     Add the name of the scene i.e. "Garage" as parameter here.
     **/
    public void OnButtonPressLoadScene(string sceneName) => SceneManager.LoadScene(sceneName);
    /***
    Enables selected menu given as string parameter, disables all else.
    Use on Button OnClick Function in Editor.
    Add the name of the menu i.e. "Level Select" as parameter.
    ***/
    public void OnButtonPressLoadMenu(string menuName)
    {
        //set inactive for all other sub-menus
        menuList.Where(n => n.name != menuName).ToList().ForEach(n => n.SetActive(false));
        //set active for selected sub-menu
        GameObject activeMenu = menuList.Find(x => x.name == menuName);
        activeMenu.SetActive(true);
    }
}
