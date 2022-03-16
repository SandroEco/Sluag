using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
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
            }

            if (gameObject.tag == ("squareShards"))
            {
                IS.squareShards++;
            }
            Destroy(gameObject);
        }
    }
}
