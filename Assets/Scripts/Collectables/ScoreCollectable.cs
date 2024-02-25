using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCollectable : Collectable
{
    [SerializeField] int toScore;
    protected override void Start()
    {
        base.Start();
    }
    protected override void ApplyEffect(Collider Col)
    {
        ScoreManager.Instance.UpdateScore(toScore);
    }
}
