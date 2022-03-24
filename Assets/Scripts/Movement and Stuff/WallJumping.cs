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

    void Start()
    {
        movementScript = GetComponent<Movement>();
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        CheckCollisions();
    }

    private void Update()
    {
        if (wallGrab)
        {
            WallGrab();
        }

        //if (Input.GetKeyUp("space") && !movementScript.isJumping)
        {
            WallJump();
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
        }
        else if(!onRightWall && movementScript.horizontalDirection <= 0f)
        {
            rb.velocity = new Vector2(-1f, rb.velocity.y);
        }

        if(onRightWall && !movementScript.isFacingRight)
        {
            //movementScript.Flip();
        }
        else if(!onRightWall && movementScript.isFacingRight)
        {
            //movementScript.Flip();
        }
    }

    void WallJump()
    {
        Vector2 jumpDirection = onRightWall ? Vector2.left : Vector2.right;
        //movementScript.Jump(Vector2.up + jumpDirection);
    }
}
