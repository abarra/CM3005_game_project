using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{

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
            ApplyEffect(Col);
            _sm.PlayCollectableSound();
            Destroy(gameObject);
        }
    }

    protected virtual void ApplyEffect(Collider Col)
    {

    }
    void RotateSelf()
    {
        transform.Rotate(0,0, rotateSpeed * Time.deltaTime);

    }
}
