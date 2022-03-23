using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rb;
    public Movement movementScript;

    public float dashingVelocity = 14f;
    public float dashingTime = 0.5f;
    private Vector2 dashingDir;
    public bool isDashing;
    public bool canDash = true;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Movement.Instance.canMove = true;
        movementScript = GetComponent<Movement>();
    }


    void Update()
    {
        if (Input.GetKeyDown("w") && canDash)
        {
            movementScript.canMove = false;
            isDashing = true;
            canDash = false;
            rb.drag = 0f;
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0f;
            dashingDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            if (dashingDir == Vector2.zero)
            {
                dashingDir = new Vector2(transform.localScale.x, 0);
            }

            StartCoroutine(StopDashing());
        }

        //anim.SetBool("isDashing", isDashing);

        if (isDashing)
        {
            rb.velocity = dashingDir.normalized * dashingVelocity;
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
        isDashing = false;
        movementScript.canMove = true;
    }
}
