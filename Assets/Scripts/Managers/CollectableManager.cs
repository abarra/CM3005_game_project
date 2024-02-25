using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableManager : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] List<GameObject> prefabs;

    [Header("Spawn Objects")]
    [SerializeField] List<GameObject> spawnPointHolders;
    void Start()
    {
        InstantiateAllSpawns();
    }

    // Update is called once per frame
    void Update()
    {

    }
    #region Initializers
    private void InstantiateAllSpawns()
    {
        for (int i = 0; i < prefabs.Count; i++)
        {
            createSpawnPointList(spawnPointHolders[i], prefabs[i]);
        }
    }
    void createSpawnPointList(GameObject parentObj, GameObject prefab)
    {
        foreach (var point in parentObj.GetComponentsInChildren<Transform>())
        {
            if (point.gameObject.CompareTag("CollectableSpawner"))
            {
                Instantiate(prefab, point);
            }
        }
    }
    #endregion
}
