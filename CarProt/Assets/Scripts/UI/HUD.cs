using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    public TextMeshProUGUI CollectedTMPro;
    public TextMeshProUGUI GearTMPro;
    [SerializeField] GameManager gameManager;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateCollected(int collected, int toCollect)
    {
        CollectedTMPro.text = $"Collected {collected}/{toCollect}";
    }

    public void UpdateGear(string current, int max)
    {
        GearTMPro.text = $"Gear:\n{current}/{max}";
    }
}
