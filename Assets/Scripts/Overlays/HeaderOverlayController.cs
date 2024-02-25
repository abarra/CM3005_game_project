using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeaderOverlayController : MonoBehaviour
{
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
}
