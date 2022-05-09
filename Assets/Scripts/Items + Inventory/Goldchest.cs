using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goldchest : MonoBehaviour
{

    private ItemDrop iD;
    private void Start()
    {
        iD = GetComponent<ItemDrop>();
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Player" && Input.GetKey(KeyCode.UpArrow))
        {
            Debug.Log("goodjob");
            //play anim
            iD.Drop();
        }
    }
}
