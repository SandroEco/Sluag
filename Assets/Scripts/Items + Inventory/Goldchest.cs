using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goldchest : MonoBehaviour
{
    private ItemDrop iD;
    private bool once;
    private Animator anim;

    private void Start()
    {
        once = true;
        iD = GetComponent<ItemDrop>();
        anim = GetComponent<Animator>();
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Player" && Input.GetAxisRaw("Vertical") == 1f)
        {
            anim.SetTrigger("Open");
            if (once)
            {
                iD.Drop();
                once = false;
            }
        }
    }
}
