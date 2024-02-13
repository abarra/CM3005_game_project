using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{

    public int emotionalValue = 0;

    protected EmotionController _ec;
    protected SoundManager _sm;

    void Start()
    {
        _ec = GameObject.Find("EmotionManager").GetComponent<EmotionController>();
        _sm = GameObject.Find("SoundManager").GetComponent<SoundManager>();

    }
 
    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision Col)
    {

        if (Col.collider.CompareTag("Player"))
        {
            _sm.PlayHitSound();

            if (emotionalValue > 0)
            {
                _ec.DecreaseSatisfuction(emotionalValue);
            }
        }
        

     
        }    
}
