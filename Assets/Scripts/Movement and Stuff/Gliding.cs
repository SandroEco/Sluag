using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gliding : MonoBehaviour
{
    private Rigidbody2D rb;
    private Movement movementScript;
    public Vector2 velocity;
    public Animator anim;

    public float glidingSpeed;
    private float initialGravityScale;

    public bool canGlide = false;

    void Start()
    {
        movementScript = GetComponent<Movement>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        initialGravityScale = rb.gravityScale;
    }

    private void Update()
    {
        Glide();
    }

    private void Glide()
    {
        if (Input.GetButton("Jump") && rb.velocity.y <= 0f)
        {
            rb.gravityScale = 0;
            rb.velocity = new Vector2(rb.velocity.x, -glidingSpeed);
            anim.SetBool("isGliding", true);
        }
        else
        {
            rb.gravityScale = initialGravityScale;
            anim.SetBool("isGliding", false);
        }
    }
}
