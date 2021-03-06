using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    private HealthAll healthAll;
    public string nameOfScene;
    public static bool canInteract;
    public bool caveForest;

    private void Awake()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
    }

    private void Start()
    {
        healthAll = GameObject.FindGameObjectWithTag("Respawn").GetComponent<HealthAll>();
        canInteract = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Color clr = this.GetComponent<SpriteRenderer>().color;
        clr.a = 0.5f;
        this.GetComponent<SpriteRenderer>().color = clr;

        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (canInteract)
        {
            if (Input.GetButton("Interact") && sceneName == "Game" || Input.GetButton("Interact") && sceneName == "Forest")
            {
                if (caveForest)
                {
                    SaveManager.instance.activeSave.tpToCaveForest = true;
                }
                healthAll = FindObjectOfType<HealthAll>();
                healthAll.lastCheckPointPos = transform.position;
                SaveManager.instance.activeSave.lastCheckPointPos = transform.position;
                SaveManager.instance.Save();
                StartCoroutine(LoadScene());
            }
            else if (Input.GetButton("Interact") && sceneName != "Game" && sceneName != "Forest")
            {
                SaveManager.instance.Save();
                StartCoroutine(LoadScene());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Color clr = this.GetComponent<SpriteRenderer>().color;
        clr.a = 1f;
        this.GetComponent<SpriteRenderer>().color = clr;
    }

    private IEnumerator LoadScene()
    {
        Movement.Instance.canMove = false;
        canInteract = false;
        Fade.instance.FadeIn();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(nameOfScene);
    }
}
