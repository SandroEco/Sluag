using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
    public InventoryScript IS;
    private BoxCollider2D bc;

    public int goldValue;
    private void Start()
    {
        IS = FindObjectOfType<InventoryScript>();
        bc = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == ("Player"))
        {
            IS.gold = IS.gold + goldValue;
            SaveManager.instance.activeSave.gold = IS.gold;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            IS.gold = IS.gold + goldValue;
            SaveManager.instance.activeSave.gold = IS.gold;
            Destroy(bc);
        }
    }
}
