using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionCollectable : Collectable
{
    [SerializeField] int emotionalValue = 0;
    protected EmotionController _ec;
    protected override void Start()
    {
        base.Start();
        collSoundIndex = 3;
        _ec = GameObject.Find("EmotionManager").GetComponent<EmotionController>();
    }
    protected override void ApplyEffect(Collider Col)
    {
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
