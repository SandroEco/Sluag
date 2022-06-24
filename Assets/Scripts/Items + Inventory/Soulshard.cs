using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soulshard : MonoBehaviour
{
    public InventoryScript IS;
    private void Start()
    {
        IS = FindObjectOfType<InventoryScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == ("Player"))
        {
            if(gameObject.tag == ("circleShards"))
            {
                IS.circleShards++;
                SaveManager.instance.activeSave.circleShards = IS.circleShards;
            }

            if (gameObject.tag == ("squareShards"))
            {
                IS.squareShards++;
                SaveManager.instance.activeSave.squareShards = IS.squareShards;
            }

            if(gameObject.tag == ("frogShards"))
            {
                IS.frogShards++;
                SaveManager.instance.activeSave.frogShards = IS.frogShards;
            }
            SaveManager.instance.Save();
            Destroy(gameObject);
        }
    }
}
