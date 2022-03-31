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
    private Rigidbody2D rb;

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
            movementScript.Die();
            StartCoroutine(DeathTransition());
        }
    }

    private IEnumerator DeathTransition()
    {
        yield return new WaitForSeconds(1);
        movementScript.canMove = true;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        player.transform.position = lastCheckPointPos;
    }
}
