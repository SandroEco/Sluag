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
            anim.SetTrigger("LightUp");
            healthAll.lastCheckPointPos = transform.position;

            SaveManager.instance.activeSave.lastCheckPointPos = transform.position;

            SaveManager.instance.Save();
        }
    }
}
