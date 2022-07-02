using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NotActiveInMenu : MonoBehaviour
{
    public GameObject UI;

    private void Awake()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if(sceneName == "Main Menu")
        {
            UI.SetActive(false);
        }
        else
        {
            UI.SetActive(true);
        }
    }

}
