using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class SpeedometerController : MonoBehaviour
{
    public TextMeshProUGUI speedText;
    public float speedMultiplier = 5.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.State == GameManager.GameState.running)
        {
            CarController car = (CarController)FindObjectOfType(typeof(CarController));
            if (car)
            {
                var nfi = new CultureInfo( "en-US", false ).NumberFormat;
                nfi.NumberDecimalDigits = 0;
                speedText.text = (car.speed * speedMultiplier).ToString("N", nfi);
            }
            else
            {
                Debug.Log("Car not found");
            }
        }
    }
}
