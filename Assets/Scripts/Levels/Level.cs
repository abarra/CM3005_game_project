using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Level : MonoBehaviour
{
    // Start is called before the first frame update
    
    protected virtual void Awake()
    {
        GameManager.OnGameOver -= ResetLevel;
        GameManager.OnGameOver += ResetLevel;

    }

    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    protected abstract void ResetLevel();

}
