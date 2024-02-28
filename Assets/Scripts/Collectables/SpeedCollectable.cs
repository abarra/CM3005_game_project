using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedCollectable : Collectable
{
    [SerializeField] float toSpeed;

    protected override void Start()
    {
        base.Start();
        collSoundIndex = 1;
    }
    protected override void ApplyEffect(Collider Col)
    {
        Col.GetComponent<CarController>().AddSpeedForTime(toSpeed);
    }
}
