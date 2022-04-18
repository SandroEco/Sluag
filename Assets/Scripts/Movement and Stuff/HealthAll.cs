using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        if (SaveManager.instance.hasLoaded)
        {
            lastCheckPointPos = SaveManager.instance.activeSave.lastCheckPointPos;
            transform.position = lastCheckPointPos;
            Debug.Log("worked");

            health = SaveManager.instance.activeSave.health;
        }
        else
        {
            SaveManager.instance.activeSave.health = health;
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
        Debug.Log("Aua");
        if(health > 0)
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
        yield return new WaitForSeconds(0.8f);
        movementScript.canMove = true;
        if(health < 1)
        {
            health = maxhealth;
        }
        player.transform.position = lastCheckPointPos;
    }

    public void toLastCheckpoint()
    {
        StartCoroutine(DeathTransition());
    }

    public IEnumerator DamageFreeze()
    {
        Time.timeScale = 0.0f;
        yield return new WaitForSecondsRealtime(0.1f);
        Time.timeScale = 1f;
    }
}
