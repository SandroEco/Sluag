using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthAll : MonoBehaviour
{
    [Header("Components")]
    public HealthBar healthBar;

    [Header("Health")]
    public int maxHealth = 30;
    public int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Debug.Log("Ded");
            //playDeathanim
            //reset to checkpoint
        }
    }
}
