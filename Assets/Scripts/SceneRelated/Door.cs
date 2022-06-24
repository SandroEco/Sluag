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
        Color clr = this.GetComponent<SpriteRenderer>().color;
        clr.a = 0.5f;
        this.GetComponent<SpriteRenderer>().color = clr;

        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (Input.GetButton("Interact") && sceneName == "Game")
        {
            healthAll = FindObjectOfType<HealthAll>();
            healthAll.lastCheckPointPos = transform.position;
            SaveManager.instance.activeSave.lastCheckPointPos = transform.position;
            SaveManager.instance.Save();
            StartCoroutine(WaitForLoad());
        }
        else if(Input.GetButton("Interact") && sceneName == "Sluag Home" || Input.GetButton("Interact") && sceneName == "Chronos Home")
        {
            SceneManager.LoadScene("Game");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Color clr = this.GetComponent<SpriteRenderer>().color;
        clr.a = 1f;
        this.GetComponent<SpriteRenderer>().color = clr;
    }

    private IEnumerator WaitForLoad()
    {
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene(nameOfScene);
    }
}
