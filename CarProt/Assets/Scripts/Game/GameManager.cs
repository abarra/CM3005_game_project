using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject playerSpawnPoint;
    [SerializeField] GameObject collectible;
    [SerializeField] UIManager UIManager;
    HUD hUD;
    List<GameObject> collectibleSpawnPoints;
    List<Collectible> collectibles;
    int toCollect = 0;
    int collected = 0;
    void Start()
    {
        hUD = UIManager.gameObject.GetComponentInChildren<HUD>();
        collectibleSpawnPoints = FindObjectsOfType<GameObject>().Where(n => n.tag == "CollectibleSpawnPoint").ToList();
        toCollect = collectibleSpawnPoints.Count;
        SpawnPlayer();
        foreach (GameObject coll in collectibleSpawnPoints)
        {
            Instantiate<GameObject>(collectible, coll.transform);
        }
        collectibles = collectibleSpawnPoints.Select(n => n.GetComponentInChildren<Collectible>()).ToList();
        hUD.UpdateCollected(collected, toCollect);
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Collectible coll in collectibles.Where(n => n != null && n.isCollected))
        {
            Destroy(coll.gameObject);
            collected++;
            hUD.UpdateCollected(collected, toCollect);
        }

        if (collectibles.Count == collected)
        {
            UIManager.SetWinMenu();
        }

    }

    void SpawnPlayer()
    {
        Vector3 spawnLoc = playerSpawnPoint.transform.position;
        player = Instantiate<GameObject>(player, new Vector3(spawnLoc.x, 2f, spawnLoc.z), Quaternion.identity);
    }

    public void ExitToMain()
    {
        SceneManager.LoadScene(0);
    }

    public void SetHUDGear(string gear, int max)
    {
        hUD.UpdateGear(gear, max);
    }
}
