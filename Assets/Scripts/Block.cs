using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public Movement movementScript;
    private Rigidbody2D rb;
    private Animator anim;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            movementScript = other.gameObject.GetComponent<Movement>();
            if(movementScript.strength != 20f)
            {
                rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            }
            else if(movementScript.strength == 20f)
            {
                rb.constraints = RigidbodyConstraints2D.None;
                anim = other.gameObject.GetComponent<Animator>();
                anim.SetBool("isPushing", true);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            anim = other.gameObject.GetComponent<Animator>();
            anim.SetBool("isPushing", false);
        }
    }
}
