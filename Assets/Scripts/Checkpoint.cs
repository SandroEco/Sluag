using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private HealthAll healthAll;
    private Animator anim;

    private void Start()
    {
        healthAll = GameObject.FindGameObjectWithTag("Respawn").GetComponent<HealthAll>();
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag ("Player"))
        {
            healthAll = FindObjectOfType<HealthAll>();
            healthAll.lastCheckPointPos = transform.position;
            anim.SetTrigger("LightUp");
        }
    }
}
