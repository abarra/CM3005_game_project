using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FooterOverlay : MonoBehaviour
{


    public void OpenMainMenu()
    {
        UIManager.Instance.ActivateView("MainMenuView");
    }
}
