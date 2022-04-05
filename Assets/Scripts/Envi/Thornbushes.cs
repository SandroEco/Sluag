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
            Debug.Log("ups");
            healthAll.TakeDamage(1);
            movementScript.Die();
            healthAll.toLastCheckpoint();
        }
    }
}
