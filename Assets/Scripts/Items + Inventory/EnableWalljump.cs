using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableWalljump : MonoBehaviour
{
    private void Awake()
    {
        if (SaveManager.instance.activeSave.enableWalljump)
        {
            FindObjectOfType<WallJumping>().enabled = true;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            FindObjectOfType<WallJumping>().enabled = true;
            SaveManager.instance.activeSave.enableWalljump = true;
            Destroy(gameObject);
        }
    }
}
