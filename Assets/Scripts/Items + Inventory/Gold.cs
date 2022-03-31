using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
    public InventoryScript IS;

    public int goldValue;
    private void Start()
    {
        IS = FindObjectOfType<InventoryScript>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == ("Player"))
        {
            IS.gold = IS.gold + goldValue;
            Destroy(gameObject);
        }
    }
}
