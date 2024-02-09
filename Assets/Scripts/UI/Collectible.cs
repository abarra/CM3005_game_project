using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public SphereCollider col;
    public bool isCollected = false;

    private void OnTriggerEnter(Collider other)
    {
        //you can change if to any collider type or get other.gameObject and compare tag, or name, or layer, or whatever you need
        if (other.GetType().ToString() == "BoxCollider")
        {
            
        }
        Debug.Log(other.GetType().ToString());
        isCollected = true;
    }
}
