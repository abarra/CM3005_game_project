using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public int emotionalValue = 0;
    public int timeValue = 0;

    protected EmotionController _ec;
    protected SoundManager _sm;

    private float rotateSpeed = 50f;
    void Start()
    {
        _ec = GameObject.Find("EmotionManager").GetComponent<EmotionController>();
        _sm = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        RotateSelf();
    }

    //Called when object is destroyed 
    void OnDestroy()
    {


    }

    void OnTriggerEnter(Collider Col)
    {
        //If player collected collactable, then destroy object 
        if (Col.CompareTag("Player"))
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

            if (timeValue > 0)
            {
                TimerManager.Instance.AddTime(timeValue);
            }

            _sm.PlayCollectableSound();
            Destroy(gameObject);
        }
    }

    protected virtual void ApplyEffect()
    {

    }
    void RotateSelf()
    {
        transform.Rotate(0,0, rotateSpeed * Time.deltaTime);

    }
}
