using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryZone : MonoBehaviour
{
    [SerializeField] Material material;
    [SerializeField, Range(0f, 1f)] float minAlpha;
    [SerializeField, Range(0f, 1f)] float maxAlpha;

    float clrChangeAmount = 0.01f;
    bool reachedMin = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ChangeMaterialAlpha();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Triggered");
            GameManager.Instance.Win();
        }
    }
    void ChangeMaterialAlpha()
    {
        if (material != null)
        {
            Color newClr = material.color;
            if (reachedMin)
            {
                newClr.a += Mathf.Clamp01(clrChangeAmount);
                if (newClr.a >= maxAlpha)
                {
                    newClr.a = maxAlpha;
                    reachedMin = false;
                }
            }
            else
            {
                newClr.a -= Mathf.Clamp01(clrChangeAmount);
                if (newClr.a <= minAlpha)
                {
                    newClr.a = minAlpha;
                    reachedMin = true;
                }
            }
            material.color = newClr;
        }
    }
}
