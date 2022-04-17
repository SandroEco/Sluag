using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJumping : MonoBehaviour
{
    public LayerMask wallLayer;
    public Rigidbody2D rb;
    public Animator anim;
    public Movement movementScript;

    [Header("Wall Jump")]
    public float wallJumpForce;
    public bool canWallJump;
    public float wallJumpDirection = -1f;
    public Vector2 wallJumpAngle;

    [Header("Wall Slide")]
    public float wallSlideSpeed;
    public bool isWallSliding;
    public float wallRaycastLength;
    public bool onWall;
    private bool onRightWall;

    void Start()
    {
        movementScript = GetComponent<Movement>();
        rb = GetComponent<Rigidbody2D>();
        canWallJump = true;
        anim = GetComponent<Animator>();
        wallJumpAngle.Normalize();
    }

    void FixedUpdate()
    {
        CheckCollisions();
    }

    private void Update()
    {
        WallSlide();
        WallJump();
    }

    void WallSlide()
    {
        if(onWall && !movementScript.isGrounded && rb.velocity.y <= 0.1)
        {
            isWallSliding = true;
            anim.SetBool("OnWall", true);
        }
        else
        {
            isWallSliding = false;
            anim.SetBool("OnWall", false);
        }

        if (isWallSliding && !movementScript.isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, wallSlideSpeed);
            movementScript.canMove = false;
        }
        else if(movementScript.isGrounded)
        {
            movementScript.canMove = true;
        }
    }

    void WallJump()
    {
        if((isWallSliding || onWall) && Input.GetKeyDown("space") && !movementScript.isGrounded)
        {
            rb.AddForce(new Vector2(wallJumpForce * wallJumpDirection * wallJumpAngle.x, wallJumpForce * wallJumpAngle.y), ForceMode2D.Impulse);
            StartCoroutine(AfterWallJump());
        }
    }

    void CheckCollisions()
    {
        onWall = Physics2D.Raycast(transform.position, Vector2.right, wallRaycastLength, wallLayer) || Physics2D.Raycast(transform.position, Vector2.left, wallRaycastLength, wallLayer);
        onRightWall = Physics2D.Raycast(transform.position, Vector2.right, wallRaycastLength, wallLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * wallRaycastLength);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.left * wallRaycastLength);
    }

    private IEnumerator AfterWallJump()
    {
        movementScript.canMove = false;
        movementScript.Flip();
        yield return new WaitForSeconds(0.3f);
        movementScript.canMove = true;
    }
}
