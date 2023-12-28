using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject[] Menus;

    void Start()
    {
        SetVisibleOnly("HUD");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

    }

    public void SetVisibleOnly(string menuName)
    {
        foreach (var menu in Menus)
        {
            menu.GetComponent<CanvasGroup>().alpha = 0f;
            menu.GetComponent<CanvasGroup>().blocksRaycasts = false;
            if (menu.gameObject.name == menuName)
            {
                menu.GetComponent<CanvasGroup>().alpha = 1f;
                menu.GetComponent<CanvasGroup>().blocksRaycasts = true;
            }
        }

    }
    public void ToggleVisible(string menuName)
    {
        GameObject menu = Menus.Where(n => n.gameObject.name == menuName).ToList()[0];
        if (menu.GetComponent<CanvasGroup>().alpha == 1f)
        {
            menu.GetComponent<CanvasGroup>().alpha = 0f;
            menu.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
        else
        {
            menu.GetComponent<CanvasGroup>().alpha = 1f;
            menu.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
    }

    public void TogglePause()
    {
        ToggleVisible("PauseMenu");
        if (Time.timeScale == 1f)
        {
            Time.timeScale = 0f;
            var pauseMenu = Menus[1].gameObject;
            pauseMenu.GetComponentInChildren<Button>().Select();
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void SetWinMenu()
    {
        SetVisibleOnly("WinMenu");
        Time.timeScale = 0f;
    }
}
