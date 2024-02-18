﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeaderOverlayController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenGarage()
    {
        UIManager.Instance.ActivateView("GarageView");
    }

    public void OpenPauseMenu()
    {
        UIManager.Instance.ActivateView("PauseMenuView");
        GameManager.Instance.PauseGame();
    }
}
