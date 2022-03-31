using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public Movement movementScript;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            movementScript = other.gameObject.GetComponent<Movement>();
            if(movementScript.strength < 20f)
            {
                rb.mass = 20f;
            }
            else
            {
                rb.mass = 1f;
            }
        }
    }
}
