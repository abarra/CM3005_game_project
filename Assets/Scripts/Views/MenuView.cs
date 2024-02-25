using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuView : View
{
    [SerializeField] List<GameObject> menuList;
    [SerializeField] AudioSource onClickSound;
    
    #region Common listeners
    public void OnButtonPressLoadMenu(string menuName)
    {
        Debug.Log($"Loading {menuName} menu");
        //set inactive for all other sub-menus
        menuList.Where(n => n.name != menuName).ToList().ForEach(n => n.SetActive(false));
        //set active for selected sub-menu
        var activeMenu = menuList.Find(x => x.name == menuName);
        if (activeMenu == null)
        {
            var menus = new List<string>();
            menuList.ForEach(n => menus.Add(n.name));
            Debug.LogError($"Can't find menu with name {menuName}. Menus: {menus.Aggregate((a, b) => a + ", " + b)}");
        }
        else
        {
            activeMenu.SetActive(true);
        }
    }

    public void PlayButtonPressSoundEffect()
    {
        if (!onClickSound.isPlaying)
        {
            onClickSound.Play();
        }
    }
    #endregion
    
    /// <summary>
    /// Start new game
    /// </summary>
    public void StartGame()
    {
        GameManager.Instance.StartGame();
        CloseView();
    }
    
    /// <summary>
    /// Restarts level
    /// </summary>
    public void RestartLevel()
    {
        GameManager.Instance.RestartLevel();
        CloseView();
    }
}