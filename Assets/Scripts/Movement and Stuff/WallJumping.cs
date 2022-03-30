using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJumping : MonoBehaviour
{
    public LayerMask wallLayer;
    public Rigidbody2D rb;

    public float wallRaycastLength;
    public bool onWall;
    private bool onRightWall;
    public Movement movementScript;

    private bool wallGrab => onWall && !movementScript.isGrounded && Input.GetKey("space");
    public bool sticksToWall;

    [Header("Wall Jump")]
    public float wallJumpForce;
    public bool canWallJump;

    void Start()
    {
        movementScript = GetComponent<Movement>();
        rb = GetComponent<Rigidbody2D>();
        canWallJump = true;
    }

    void FixedUpdate()
    {
        CheckCollisions();
        if (wallGrab)
        {
            WallGrab();
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp("space") && sticksToWall && canWallJump)
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
        }
        else if(!onRightWall && movementScript.horizontalDirection <= 0f)
        {
            rb.velocity = new Vector2(-1f, rb.velocity.y);
            sticksToWall = true;
        }
        else
        {
            sticksToWall = false;
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
        if (onWall)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce((Vector2.up + Vector2.right).normalized * wallJumpForce, ForceMode2D.Impulse);
        }
        if (onRightWall)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce((Vector2.up + Vector2.left).normalized * wallJumpForce, ForceMode2D.Impulse);
        }
    }
}
