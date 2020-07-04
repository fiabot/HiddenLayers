using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteMenu : MonoBehaviour
{
    public static LevelCompleteMenu instance;
    public GameObject menu;

    public void Awake()
    {
        instance = this;
        //menu.SetActive(false);
    }

    public void OnDestroy()
    {
        instance = null;
    }

    public void showMenu()
    {
        Debug.Log("Showing Menu");
        menu.SetActive(true);
    }

    public void loadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        /*if(SceneManager.GetActiveScene().buildIndex <= SceneManager.sceneCount)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }*/

    }
}
