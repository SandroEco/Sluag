using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthAll : MonoBehaviour
{
    [Header("Components")]
    public Movement movementScript;
    public GameObject player;

    [Header("Health")]
    public int health;
    public int maxhealth;

    public Image[] heartsImages;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    [Header("Checkpoint")]
    public Vector3 lastCheckPointPos;

    void Start()
    {
        movementScript = GetComponent<Movement>();
        player = GameObject.FindGameObjectWithTag("Player");
        Scene currentScene = SceneManager.GetActiveScene();

        string sceneName = currentScene.name;

        if (SaveManager.instance.hasLoaded)
        {
            health = SaveManager.instance.activeSave.health;
        }
        else
        {
            SaveManager.instance.activeSave.health = health;
        }

        if (SaveManager.instance.hasLoaded && sceneName == "Game")
        {
            lastCheckPointPos = SaveManager.instance.activeSave.lastCheckPointPos;
            transform.position = lastCheckPointPos;
        }

        if(health == 0)
        {
            health = maxhealth;
        }
    }

    private void Update()
    {
        movementScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
        player = GameObject.FindGameObjectWithTag("Player");

        if(health > maxhealth)
        {
            health = maxhealth;
        }

        for(int i = 0; i < heartsImages.Length; i++)
        {
            if(i < health)
            {
                heartsImages[i].sprite = fullHeart;
            }
            else
            {
                heartsImages[i].sprite = emptyHeart;
            }

            if(i < maxhealth)
            {
                heartsImages[i].enabled = true;
            }
            else
            {
                heartsImages[i].enabled = false;
            }
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        SaveManager.instance.activeSave.health = health;

        if (health >= 1)
        {
            movementScript.Damaged();
            StartCoroutine(DamageFreeze());
        }

        if (health <= 0)
        {
            movementScript.Die();
            StartCoroutine(DeathTransition());
        }
    }

    public IEnumerator DeathTransition()
    {
        if (health < 1)
        {
            health = maxhealth;
        }
        yield return new WaitForSeconds(1f);
        movementScript.isDying = false;
        player.transform.position = lastCheckPointPos;
        movementScript.canMove = true;
    }

    public void toLastCheckpoint()
    {
        StartCoroutine(DeathTransition());
    }

    public IEnumerator DamageFreeze()
    {
        Time.timeScale = 0.0f;
        yield return new WaitForSecondsRealtime(0.4f);
        Time.timeScale = 1f;
    }
}
