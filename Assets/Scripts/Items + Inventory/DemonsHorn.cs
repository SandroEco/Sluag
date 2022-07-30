using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonsHorn : MonoBehaviour
{
    private BoxCollider2D bc;
    public AudioSource collectSource;

    private void Start()
    {
        bc = GetComponent<BoxCollider2D>();
        collectSource = GameObject.Find("SpecialCollectSource").GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            collectSource.Play();
            InventoryScript.instance.demonsHorn++;
            SaveManager.instance.activeSave.demonsHorn = InventoryScript.instance.demonsHorn;
            Destroy(gameObject);
        }
    }
}
