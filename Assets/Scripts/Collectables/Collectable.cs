using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] ParticleSystem destructionParticles;
    protected SoundManager _sm;
    protected int collSoundIndex = 0; 
    private float rotateSpeed = 50f;
    protected virtual void Start()
    {
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
            _sm.PlayCollectableSound(collSoundIndex);
            Instantiate(destructionParticles, transform.position, Quaternion.identity);
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
