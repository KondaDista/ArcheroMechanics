using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Levels : MonoBehaviour
{
    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
    
    public void Level()
    {
        SceneManager.LoadScene(1);
    }
}
