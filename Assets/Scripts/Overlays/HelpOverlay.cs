using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpOverlay : MonoBehaviour
{
    public GameObject helpWindow;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            ToggleVisibility();
        }
    }

    public void ToggleVisibility()
    {
        if (helpWindow.activeSelf)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    public void Show()
    {
        Debug.Log("[HelpOverlay] Showing");
        helpWindow.SetActive(true);
    }

    public void Hide()
    {
        Debug.Log("[HelpOverlay] Hiding");
        helpWindow.SetActive(false);
    }
}
