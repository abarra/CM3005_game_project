using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCollectable : Collectable
{
    [SerializeField] int timeValue;

    protected override void ApplyEffect(Collider Col)
    {
        if (timeValue > 0)
        {
            TimerManager.Instance.AddTime(timeValue);
        }
    }

}
