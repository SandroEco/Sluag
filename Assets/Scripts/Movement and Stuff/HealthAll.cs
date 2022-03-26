using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthAll : MonoBehaviour
{
    [Header("Components")]
    public HealthBar healthBar;
    public Movement movementScript;
    public GameObject player;

    [Header("Health")]
    public int maxHealth = 30;
    public int currentHealth;

    [Header("Checkpoint")]
    public Vector2 lastCheckPointPos;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        movementScript = GetComponent<Movement>();
    }

    private void Update()
    {
        movementScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            player.transform.position = lastCheckPointPos;
            movementScript.Die();
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);
        }
    }
}
