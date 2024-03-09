using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLevel : MonoBehaviour
{
    public void LoadLevelOne()
    {
        // Load level scene
        SceneManager.LoadScene(1);
        // Start game
        GameManager.Instance.StartGame();
    }
    
    public void LoadLevelTwo()
    {
        // Not implemented
    }
}
