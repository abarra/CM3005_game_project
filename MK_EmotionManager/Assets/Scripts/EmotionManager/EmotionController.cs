using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// this script will store the value of Emotional Satifaction
// change satisfaction events
// Increase/Decrease satifaction level
// Comminucate to UI to show the current level (maybe using  some sort of events system)

public class EmotionController : MonoBehaviour
{

    public static EmotionController instance = null;


    public int satisfactionPoint;
    
    private enum SatisfactionLevels
    {
          Low,
          Medium,
          High
    }
    [SerializeField]
    private SatisfactionLevels satisfactionLevel;

    public int lowTheshold = 45;

    public int mediumTheshold = 75;


  

    /* fire each time saisfaction level is incresed */
    public delegate void SatisfactionIncreased(int points);

    public  static event SatisfactionIncreased OnSatisfactionIncreased;


    /* fire each time saisfaction level is decreased */
    public delegate void SatisfactionDecreased(int points);

    public static event SatisfactionDecreased OnSatisfactionDecreased;


    /* Satisfaction Level changed */
    public delegate void SatisfactionLevelChanged(bool isDecreased, string prevLevel, string newLevel);

    public static event SatisfactionLevelChanged OnSatisfactionLevelChanged;


    /* Satisfaction Level changed */
    public delegate void ZeroLevelReached();

    public static event ZeroLevelReached OnZeroLevelReached;


    void Awake()
    {
        // the EmotionController is a Singleton
        // store this as the instance of this object
       
        if (instance == null)
        { 
            instance = this; 
        }
        else if (instance == this)
        { 
            Destroy(gameObject); 
        }

        DontDestroyOnLoad(gameObject);


    }



    private void Start()
    {
        satisfactionLevel = SatisfactionLevels.High;
        satisfactionPoint = 100;
    }
    public void IncreaseSatisfuction(int amount) {
        int prevSatisfactionPoint = satisfactionPoint;
        string prevSatisfactionLevel = satisfactionLevel.ToString();
        int newSatisfactionPoint = Math.Min(satisfactionPoint + amount, 100);
        satisfactionPoint = newSatisfactionPoint;

        // fire change points events 
        OnSatisfactionIncreased? .Invoke(newSatisfactionPoint);
       
        // check if the Satisfaction level should be changed
        switch (satisfactionLevel) {
            case SatisfactionLevels.Medium:
                if (newSatisfactionPoint > mediumTheshold) {
                    // we got to  High satisfaction  zone
                    satisfactionLevel = SatisfactionLevels.High;
                    // fire corresponding event
                    OnSatisfactionLevelChanged?.Invoke(false, prevSatisfactionLevel, satisfactionLevel.ToString());
                }
                break;
            case SatisfactionLevels.Low:
                if (newSatisfactionPoint > mediumTheshold)
                {
                    // we got to  High satisfaction  zone
                    satisfactionLevel = SatisfactionLevels.High;
                    // fire corresponding event
                    OnSatisfactionLevelChanged?.Invoke(false, prevSatisfactionLevel, satisfactionLevel.ToString());
                }
                else if (newSatisfactionPoint > lowTheshold) {

                    // we got to  Medium satisfaction  zone
                    satisfactionLevel = SatisfactionLevels.Medium;
                    // fire corresponding event
                    OnSatisfactionLevelChanged?.Invoke(false, prevSatisfactionLevel, satisfactionLevel.ToString());
                }
                break;

        }

    }

    public void DecreaseSatisfuction(int amount)
    {
        int prevSatisfactionPoint = satisfactionPoint;
        string prevSatisfactionLevel = satisfactionLevel.ToString();
        int newSatisfactionPoint = Math.Max(satisfactionPoint - amount, 0);
        satisfactionPoint = newSatisfactionPoint;

        // fire change points events 
        OnSatisfactionDecreased?.Invoke(newSatisfactionPoint);

        // check if the Satisfaction level should be changed
        switch (satisfactionLevel)
        {
            case SatisfactionLevels.Medium:
                if (newSatisfactionPoint < lowTheshold)
                {
                    // we got to  High satisfaction  zone
                    satisfactionLevel = SatisfactionLevels.Low;
                    // fire corresponding event
                    OnSatisfactionLevelChanged?.Invoke(true, prevSatisfactionLevel, satisfactionLevel.ToString());
                }
                break;
            case SatisfactionLevels.High:
                if (newSatisfactionPoint < lowTheshold)
                {
                    // we got to  High satisfaction  zone
                    satisfactionLevel = SatisfactionLevels.Low;
                    // fire corresponding event
                    OnSatisfactionLevelChanged?.Invoke(true, prevSatisfactionLevel, satisfactionLevel.ToString());
                }
                else if (newSatisfactionPoint < mediumTheshold )
                {

                    // we got to  Medium satisfaction  zone
                    satisfactionLevel = SatisfactionLevels.Medium;
                    // fire corresponding event
                    OnSatisfactionLevelChanged?.Invoke(true, prevSatisfactionLevel, satisfactionLevel.ToString());
                }
                break;

        }


        if (satisfactionLevel == 0) {
            Debug.Log("Satisfaction level is too low. Game is over!");
            OnZeroLevelReached? .Invoke();
        }
    }

    private void Update()
    {
        /* TEST CODE */

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.DownArrow)){

            DecreaseSatisfuction(10);

        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {

            IncreaseSatisfuction(10);

        }

        /**    **/
    }


}
