using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{

    public int emotionalValue = 0;

    protected EmotionController _ec;

    void Start()
    {
        _ec = GameObject.Find("EmotionManager").GetComponent<EmotionController>();


    }
 
    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision Col)
    {
       
            if (emotionalValue > 0)
            {
                _ec.DecreaseSatisfuction(emotionalValue);
            }
        }    
}
