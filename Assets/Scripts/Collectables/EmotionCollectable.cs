using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionCollectable : Collectable
{
    public int emotionalValue = 0;
    protected override void ApplyEffect(Collider Col)
    {
        // the collectable is collected 
        // it might have emotional impact
        Debug.Log("emotional value");
        Debug.Log(emotionalValue);


        if (emotionalValue > 0)
        {
            _ec.IncreaseSatisfuction(emotionalValue);
        }
        else if (emotionalValue < 0)
        {
            _ec.DecreaseSatisfuction(emotionalValue);
        }
    }
}
