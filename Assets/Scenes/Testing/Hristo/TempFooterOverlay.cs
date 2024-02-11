using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempFooterOverlay : MonoBehaviour
{
    public void OpenMainMenu()
    {
        UIManager.Instance.ActivateView("MainMenu");
    }
}
