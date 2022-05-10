using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goldchest : MonoBehaviour
{

    private ItemDrop iD;
    private bool once;

    private void Start()
    {
        once = true;
        iD = GetComponent<ItemDrop>();
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Player" && Input.GetKey(KeyCode.UpArrow))
        {
            Debug.Log("goodjob");
            //play anim
            if (once)
            {
                iD.Drop();
                once = false;
            }
        }
    }
}
