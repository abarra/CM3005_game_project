using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLevel : MonoBehaviour
{
    public void LoadLevelOne()
    {
        SceneManager.LoadScene(1);
    }
    
    public void LoadLevelTwo()
    {
        SceneManager.LoadScene(2);
    }
}
