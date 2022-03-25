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

    [Header("Layermasks")]
    public LayerMask groundLayer;
    public LayerMask playerLayer;

    public enemyHealth enemyHealth;


    public float walkSpeed;
    public bool mustPatrol;
    private bool mustFlip;

    public float maxDistance;
    private float oldPos = 0.0f;
    public bool isFacingRight;

    public int attackDamage;
        
    void Start()
    {
        mustPatrol = true;
        oldPos = transform.position.x;
    }

    void Update()
    {
        CheckForPlayer();

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

    void CheckForPlayer()
    {
        if(transform.position.x > oldPos)
        {
            isFacingRight = true;
        }
        else if(transform.position.x < oldPos)
        {
            isFacingRight = false;
        }
        oldPos = transform.position.x;

        if (isFacingRight)
        {
            if (Physics2D.Raycast(transform.position, Vector3.right, maxDistance, playerLayer))
            {
                Debug.Log("found");
                Debug.DrawRay(transform.position, Vector3.right * maxDistance, Color.red);
                RunTowardsPlayer();
            }
            else
            {
                anim.SetBool("MaxSpeedReached", false);
            }

        }

        if (!isFacingRight)
        {
            if(Physics2D.Raycast(transform.position, Vector3.left, maxDistance, playerLayer))
            {
                Debug.Log("found");
                Debug.DrawRay(transform.position, Vector3.left * maxDistance, Color.red);
                RunTowardsPlayer();
            }
            else
            {
                anim.SetBool("MaxSpeedReached", false);
            }
        }
    }

    void RunTowardsPlayer()
    {
        anim.SetBool("MaxSpeedReached", true);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            var healthComponent = collision.gameObject.GetComponent<PlayerHealth>();
            if (healthComponent != null)
            {
                healthComponent.TakeDamage(attackDamage);
                //dicker Knockback
                Debug.Log("Aua");
            }
        }
    }
}
