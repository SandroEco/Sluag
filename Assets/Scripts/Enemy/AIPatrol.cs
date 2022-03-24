using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrol : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody2D rb;
    public Transform groundCheckPos;
    public Animator anim;
    public PolygonCollider2D bodyCollider;
    public enemyHealth enemyHealth;

    [Header("Layermasks")]
    public LayerMask groundLayer;

    public float walkSpeed;
    public bool mustPatrol;
    private bool mustFlip;

    
    void Start()
    {
        mustPatrol = true;
    }

    void Update()
    {
        if (mustPatrol && !enemyHealth.isKnockbacked)
        {
            Patrol();
        }
    }

    private void FixedUpdate()
    {
        if (mustPatrol)
        {
            mustFlip = !Physics2D.OverlapCircle(groundCheckPos.position, 0.1f, groundLayer);
        }
    }

    void Patrol()
    {
        if (mustFlip || bodyCollider.IsTouchingLayers(groundLayer))
        {
            Flip();
        }
        rb.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, rb.velocity.y);
        anim.SetFloat("speed", 1);
    }

    void Flip()
    {
            mustPatrol = false;
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
            walkSpeed *= -1;
            mustPatrol = true;
    }

}
