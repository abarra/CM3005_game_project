using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public SphereCollider col;
    public bool isCollected = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetType().ToString() == "BoxCollider")
        {
            
        }
        Debug.Log(other.GetType().ToString());
        isCollected = true;
    }
}
