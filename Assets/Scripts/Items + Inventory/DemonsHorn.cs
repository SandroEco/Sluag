using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonsHorn : MonoBehaviour
{
    private BoxCollider2D bc;

    private void Start()
    {
        bc = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            InventoryScript.instance.demonsHorn++;
            SaveManager.instance.activeSave.demonsHorn = InventoryScript.instance.demonsHorn;
            Destroy(gameObject);
        }
    }
}
