using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    private Animator anim;
    SFXController sfx;
    public bool opened = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
        sfx = GetComponent<SFXController>();
        if(SaveManager.instance.activeSave.opened == true)
        {
            Destroy(gameObject);
        }
        else
        {
            return;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Player" && Input.GetButton("Interact") && InventoryScript.instance.key >= 1)
        {
            sfx.OpenChestSound();
            anim.SetTrigger("Opened");
            InventoryScript.instance.key-= 1;
            SaveManager.instance.activeSave.opened = true;
            SaveManager.instance.activeSave.key = InventoryScript.instance.key;
        }
    }
}
