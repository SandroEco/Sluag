using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneDontMove : MonoBehaviour
{
    private Movement movement;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            movement = other.GetComponent<Movement>();
            movement.canMove = false;
        }
    }
}
