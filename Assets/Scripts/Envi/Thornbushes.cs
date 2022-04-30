using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thornbushes : MonoBehaviour
{
    public HealthAll healthAll;
    public Movement movementScript;

    private void Start()
    {
        healthAll = FindObjectOfType<HealthAll>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == ("Player"))
        {
            movementScript = FindObjectOfType<Movement>();
            healthAll.health -= 1;
            SaveManager.instance.activeSave.health = healthAll.health;
            movementScript.Die();
            healthAll.toLastCheckpoint();
        }
    }
}
