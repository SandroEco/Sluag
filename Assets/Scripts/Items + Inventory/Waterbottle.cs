using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waterbottle : MonoBehaviour
{
    private BoxCollider2D bc;

    private void Start()
    {
        bc = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            InventoryScript.instance.waterBottle++;
            SaveManager.instance.activeSave.waterBottle = InventoryScript.instance.waterBottle;
            Destroy(gameObject);
        }
    }
}
