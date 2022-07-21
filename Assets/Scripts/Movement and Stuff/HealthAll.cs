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
    public Invulnerability invulnerability;
    public SFXController sfx;

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

        if (SaveManager.instance.hasLoaded && sceneName == "Game" || SaveManager.instance.hasLoaded && sceneName == "Forest")
        {
            lastCheckPointPos = SaveManager.instance.activeSave.lastCheckPointPos;
            transform.position = lastCheckPointPos;
            if (sceneName == "Forest" && SaveManager.instance.activeSave.tpToCaveForest == true)
            {
                Debug.Log("worked");
                transform.position = new Vector2(224.5f, -23);
                SaveManager.instance.activeSave.tpToCaveForest = false;
            }
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
        invulnerability = GameObject.FindGameObjectWithTag("Player").GetComponent<Invulnerability>();

        if (health > maxhealth)
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
        StartCoroutine(invulnerability.GetInvulnerable());

        if (health >= 1)
        {
            sfx.GotHitSound();
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
        yield return new WaitForSecondsRealtime(0.3f);
        Time.timeScale = 1f;
    }
}
