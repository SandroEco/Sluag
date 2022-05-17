using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    private HealthAll healthAll;
    public string nameOfScene;

    private void Start()
    {
        healthAll = GameObject.FindGameObjectWithTag("Respawn").GetComponent<HealthAll>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (Input.GetButton("Interact") && sceneName == "Game")
        {
            healthAll = FindObjectOfType<HealthAll>();
            healthAll.lastCheckPointPos = transform.position;
            SaveManager.instance.activeSave.lastCheckPointPos = transform.position;
            SaveManager.instance.Save();
            SceneManager.LoadScene(nameOfScene);
        }
        else if(Input.GetButton("Interact") && sceneName == "Sluag Home")
        {
            SceneManager.LoadScene("Game");
        }
    }
}
