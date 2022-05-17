using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rb;
    public Movement movementScript;
    private Animator anim;
    //public GameObject destroyEffect;

    public float dashingVelocity = 14f;
    public float dashingTime = 0.5f;
    private float dashingDir;
    public bool isDashing;
    public bool canDash = true;
    private float timeBtwDash;
    public float dashCooldown;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Movement.Instance.canMove = true;
        movementScript = GetComponent<Movement>();
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        if(timeBtwDash <= 0)
        {
            if (Input.GetButtonDown("Special") && canDash)
            {
                movementScript.canMove = false;
                isDashing = true;
                canDash = false;
                rb.drag = 0f;
                rb.velocity = Vector2.zero;
                rb.gravityScale = 0f;
                dashingDir = (int)Input.GetAxisRaw("Horizontal");

                if (movementScript.isFacingRight)
                {
                    dashingDir = (1);
                }
                if (!movementScript.isFacingRight)
                {
                    dashingDir = (-1);
                }

                if (timeBtwDash <= 0)
                {
                    timeBtwDash = dashCooldown;
                }

                StartCoroutine(StopDashing());
            }
        }
        else
        {
            timeBtwDash -= Time.deltaTime;
        }

        if (isDashing)
        {
            anim.SetBool("isDashing", true);
            rb.velocity = transform.right * dashingDir * dashingVelocity;
            return;
        }

        if (movementScript.isGrounded)
        {
            canDash = true;
        }

    }

    private IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(dashingTime);
        if(rb.velocity.y > 1f)
        {
            rb.velocity = new Vector2(transform.position.x, 20f);
        }
        isDashing = false;
        movementScript.canMove = true;
        anim.SetBool("isDashing", false);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (isDashing)
        {
            if (other.gameObject.CompareTag("Environment"))
            {
                Destroy(other.gameObject);
                //Instantiate(destroyEffect, transform.position, Quaternion.identity);
            }
        }
    }
}
