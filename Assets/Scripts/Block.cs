using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public Movement movementScript;
    private Rigidbody2D rb;
    public Animator anim;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "BearAntlers")
        {
            movementScript = other.gameObject.GetComponentInParent(typeof(Movement)) as Movement;
            if(movementScript.strength != 20f)
            {
                rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            }
            else if(movementScript.strength == 20f)
            {
                rb.constraints = RigidbodyConstraints2D.None;
                anim = other.gameObject.GetComponentInParent(typeof(Animator)) as Animator;
                anim.SetBool("isPushing", true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "BearAntlers")
        {
            anim = other.gameObject.GetComponentInParent(typeof(Animator)) as Animator;
            anim.SetBool("isPushing", false);
        }
    }
}
