using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public InventoryScript IS;
    private CapsuleCollider2D cc;
    void Start()
    {
        IS = FindObjectOfType<InventoryScript>();
        cc = GetComponent<CapsuleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IS.key++;
            SaveManager.instance.activeSave.key = IS.key;
            Destroy(gameObject);
        }
    }
}
