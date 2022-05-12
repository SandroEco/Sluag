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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            IS.key++;
            SaveManager.instance.activeSave.key = IS.key;
            Destroy(gameObject);
        }
    }
}
