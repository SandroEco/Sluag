using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    private Animator anim;
    SFXController sfx;

    private void Start()
    {
        anim = GetComponent<Animator>();
        sfx = GetComponent<SFXController>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Player" && Input.GetAxisRaw("Vertical") == 1f && InventoryScript.instance.key >= 1)
        {
            sfx.OpenChestSound();
            anim.SetTrigger("Opened");
            InventoryScript.instance.key-= 1;
            SaveManager.instance.activeSave.key = InventoryScript.instance.key;
        }
    }
}
