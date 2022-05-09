 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRun : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody2D rb;
    private Animator anim;


    [Header("LayerMask")]
    public LayerMask wallLayer;

    [Header("Wall Detection")]
    public float wallRaycastLength;
    public bool onWall;
    public bool onRightWall;

    private bool wallGrab => onWall && !movementScript.isGrounded && Input.GetKey("space") && !wallRun;

    [Header("Wall Run")]
    public Movement movementScript;
    public float wallRunModifier = 0.85f;
    private bool wallRun => onWall && Input.GetKey(KeyCode.UpArrow);

    private void Start()
    {
        movementScript = GetComponent<Movement>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        CheckCollisions();
        if (wallGrab) WallGrab();
        if (wallRun)
        {
            Wallrun();
        }
        else
        {
            anim.SetBool("isWallRunning", false);
            anim.SetBool("isRightWallRunning", false);
        }

    }

    void StickToWall()
    {
        if (onRightWall && movementScript.horizontalDirection >= 0f)
        {
            rb.velocity = new Vector2(1f, rb.velocity.y);
        }
        else if (!onRightWall && movementScript.horizontalDirection <= 0f)
        {
            rb.velocity = new Vector2(-1f, rb.velocity.y);
        }

        if (onRightWall && !movementScript.isFacingRight)
        {
            movementScript.Flip();
        }
        else if (!onRightWall && movementScript.isFacingRight)
        {
            movementScript.Flip();
        }
    }

    void WallGrab()
    {
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        StickToWall();
    }

    void Wallrun()
    {
        if(onWall && !onRightWall)
        {
            rb.velocity = new Vector2(rb.velocity.x, movementScript.maxMoveSpeed * wallRunModifier);
            anim.SetBool("isWallRunning", true);
            StickToWall();
        }
        else if(onWall && onRightWall)
        {
            rb.velocity = new Vector2(rb.velocity.x, movementScript.maxMoveSpeed * wallRunModifier);
            anim.SetBool("isRightWallRunning", true);
            StickToWall();
        }
    }

    void CheckCollisions()
    {
        onWall = Physics2D.Raycast(transform.position + new Vector3(0f, -0.8f, 0f), Vector2.right, wallRaycastLength, wallLayer) || Physics2D.Raycast(transform.position + new Vector3(0f, -0.8f, 0f), Vector2.left, wallRaycastLength, wallLayer);
        onRightWall = Physics2D.Raycast(transform.position + new Vector3(0f, -0.8f, 0f), Vector2.right, wallRaycastLength, wallLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position + new Vector3(0f, -0.8f, 0f), transform.position + new Vector3(0f, -0.8f, 0f) + Vector3.right * wallRaycastLength);
        Gizmos.DrawLine(transform.position + new Vector3(0f, -0.8f, 0f), transform.position + new Vector3(0f, -0.8f, 0f) + Vector3.left * wallRaycastLength);
    }

}
