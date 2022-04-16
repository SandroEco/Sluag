using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJumping : MonoBehaviour
{
    public LayerMask wallLayer;
    public Rigidbody2D rb;
    public Animator anim;

    public float wallRaycastLength;
    public bool onWall;
    private bool onRightWall;
    public Movement movementScript;

    private bool wallGrab => onWall && !movementScript.isGrounded && Input.GetKeyDown("space");
    public bool sticksToWall;

    [Header("Wall Jump")]
    public float wallJumpForce;
    public bool canWallJump;

    void Start()
    {
        movementScript = GetComponent<Movement>();
        rb = GetComponent<Rigidbody2D>();
        canWallJump = true;
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        CheckCollisions();
        /*
        if (wallGrab)
        {
            WallGrab();
        }
        */
    }

    private void Update()
    {
        if (onWall && !movementScript.isGrounded)
        {
            anim.SetBool("OnWall", true);
        }
        else
        {
            anim.SetBool("OnWall", false);
        }

        if (Input.GetKeyUp("space") && canWallJump)
        {
            WallJump();
            canWallJump = false;
        }
        if (movementScript.isGrounded)
        {
            canWallJump = true;
        }
    }

    void CheckCollisions()
    {
        onWall = Physics2D.Raycast(transform.position, Vector2.right, wallRaycastLength, wallLayer) || Physics2D.Raycast(transform.position, Vector2.left, wallRaycastLength, wallLayer);
        onRightWall = Physics2D.Raycast(transform.position, Vector2.right, wallRaycastLength, wallLayer);
    }

    void WallGrab()
    {
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        StickToWall();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * wallRaycastLength);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.left * wallRaycastLength);
    }

    void StickToWall()
    {
        if(onRightWall && movementScript.horizontalDirection >= 0f)
        {
            rb.velocity = new Vector2(1f, rb.velocity.y);
            sticksToWall = true;
            anim.SetBool("OnWall", true);
        }
        else if(!onRightWall && movementScript.horizontalDirection <= 0f)
        {
            rb.velocity = new Vector2(-1f, rb.velocity.y);
            sticksToWall = true;
            anim.SetBool("OnWall", true);
        }
        else
        {
            sticksToWall = false;
            anim.SetBool("OnWall", false);
        }

        if (onRightWall && !movementScript.isFacingRight)
        {
            movementScript.Flip();
        }
        else if(!onRightWall && movementScript.isFacingRight)
        {
            movementScript.Flip();
        }
    }

    void WallJump()
    {
        if (onWall && !onRightWall)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce((Vector2.up + Vector2.right).normalized * wallJumpForce, ForceMode2D.Impulse);
        }
        if (onRightWall && onRightWall)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce((Vector2.left + Vector2.up).normalized * wallJumpForce, ForceMode2D.Impulse);
        }
    }
}
