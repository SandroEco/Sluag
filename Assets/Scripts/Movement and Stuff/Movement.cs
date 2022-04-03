using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public static Movement Instance { get; private set; }
    public SceneDetails CurrentScene { get; private set; }
    public SceneDetails PrevScene { get; private set; }

    [Header("Components")]
    private Rigidbody2D rb;
    private BoxCollider2D bc;
    private Animator anim;
    private Acceleration accelerationScript;

    [Header("Layer Masks")]
    [SerializeField]private LayerMask groundLayer;

    [Header("Movement Variables")]
    public float movementAcceleration;
    public float maxMoveSpeed;
    public float movementDeceleration;
    public float horizontalDirection;
    private bool changingDirection => (rb.velocity.x > 0f && horizontalDirection < 0f) || (rb.velocity.x < 0f && horizontalDirection > 0f);
    public bool isFacingRight = true;
    public bool canMove = true;
       
    [Header("Jump Variables")]
    public float jumpForce = 12f;
    public float airLinearDrag = 2.5f;
    public float fallMultiplier = 8f;
    public float lowJumpFallMultiplier = 5f;
    private float coyoteTime = 0.05f;
    private float coyoteTimeCounter;
    private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;
    private bool canJump => jumpBufferCounter > 0f && coyoteTimeCounter > 0f;

    [Header("Ground Collision Variables")]
    public float groundRaycastLength;
    public bool isGrounded;

    [Header("Particles")]
    public Transform dustJump;
    private bool spawnDust;

    [Header("Corner Correction Variables")]
    [SerializeField] private float topRaycastLength;
    [SerializeField] private Vector3 edgeRaycastOffset;
    [SerializeField] private Vector3 innerRaycastOffset;
    private bool canCornerCorrect;

    [Header("Others")]
    public float strength;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        accelerationScript = GetComponent<Acceleration>();

        canMove = true;
    }

    void Update()
    {
        #region Movement
        horizontalDirection = GetInput().x;
        #endregion 

        #region CoyoteTime
        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            coyoteTimeCounter = 0f;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }
        #endregion

        #region Particles
        if (isGrounded == true)
        {
            if (spawnDust == true)
            {
                Instantiate(dustJump, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                spawnDust = false;
            }
        }
        else
        {
            spawnDust = true;
        }
        #endregion
    }

    private void FixedUpdate()
    {
        CheckCollisions();
        MoveCharacter();
        #region Jumping
        if (isGrounded)
        {
            anim.SetBool("isFalling", false);
            anim.SetBool("isJumping", false);
            ApplyGroundLinearDrag();
        }
        else
        {
            ApplyAirLinearDrag();
            anim.SetBool("isJumping", true);
            FallMultiplier();
        }
        if (canJump)
        {
            jumpBufferCounter = 0f;
            anim.SetTrigger("takeOf");
            Jump();
        }
        #endregion
        /*
        if (canCornerCorrect)
        {
            CornerCorrect(rb.velocity.y);
        }
        */
    }

    public void SetCurrentScene(SceneDetails currScene)
    {
        PrevScene = CurrentScene;
        CurrentScene = currScene;
    }

    private Vector2 GetInput()
    {
        return new Vector2 (Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
    private void MoveCharacter()
    {
        if(canMove)
        {
            rb.AddForce(new Vector2(horizontalDirection, 0f) * movementAcceleration);
            anim.SetFloat("speed", Mathf.Abs(horizontalDirection));

            if (isFacingRight && horizontalDirection < 0 || !isFacingRight && horizontalDirection > 0)
            {
                Flip();
            }

            if (Mathf.Abs(rb.velocity.x) > maxMoveSpeed)
            {
                if (accelerationScript)
                {
                    accelerationScript.setMaxSpeedReached = true;

                }
                rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxMoveSpeed, rb.velocity.y);
            }
            else
            {
                if (accelerationScript)
                {
                    accelerationScript.setMaxSpeedReached = false;
                }
            }
        }
    }

    public void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    }

    public void ApplyAirLinearDrag()
    {
        rb.drag = airLinearDrag;
    }

    private void ApplyGroundLinearDrag()
    {
        if(Mathf.Abs(horizontalDirection) < 0.4f || changingDirection)
        {
            rb.drag = movementDeceleration;
        }
        else
        {
            rb.drag = 0f;
        }
    }

    public void Jump()
    {
        ApplyAirLinearDrag();
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void FallMultiplier()
    {
        if(rb.velocity.y < 0)
        {
            rb.gravityScale = fallMultiplier;
            anim.SetBool("isFalling", true);
        }
        else if(rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.gravityScale = lowJumpFallMultiplier;
        }
        else
        {
            rb.gravityScale = 1f;
        }
    }

    /*
    void CornerCorrect(float Yvelocity)
    {
        //Push player to left
        RaycastHit2D hit = Physics2D.Raycast(transform.position - innerRaycastOffset + Vector3.up * topRaycastLength, Vector3.left, topRaycastLength, groundLayer);
        if(hit.collider != null)
        {
            float newPos = Vector3.Distance(new Vector3(hit.point.x, transform.position.y, 0f) + Vector3.up * topRaycastLength, transform.position - edgeRaycastOffset + Vector3.up * topRaycastLength);
            transform.position = new Vector3(transform.position.x + newPos, transform.position.y, transform.position.z);
            rb.velocity = new Vector2(rb.velocity.x, Yvelocity);
            return;
        }

        //Push player to right

        hit = Physics2D.Raycast(transform.position + innerRaycastOffset + Vector3.up * topRaycastLength, Vector3.right, topRaycastLength, groundLayer);
        if (hit.collider != null)
        {
            float newPos = Vector3.Distance(new Vector3(hit.point.x, transform.position.y, 0f) + Vector3.up * topRaycastLength, transform.position + edgeRaycastOffset + Vector3.up * topRaycastLength);
            transform.position = new Vector3(transform.position.x - newPos, transform.position.y, transform.position.z);
            rb.velocity = new Vector2(rb.velocity.x, Yvelocity);
        }
    }
    */

    private void CheckCollisions()
    {
        isGrounded = Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, groundRaycastLength, Vector2.down, groundRaycastLength, groundLayer);
        Debug.DrawRay(bc.bounds.center + new Vector3(bc.bounds.extents.x, 0), Vector2.down * (bc.bounds.extents.y + groundRaycastLength), Color.red);
        Debug.DrawRay(bc.bounds.center - new Vector3(bc.bounds.extents.x, 0), Vector2.down * (bc.bounds.extents.y + groundRaycastLength), Color.red);
        Debug.DrawRay(bc.bounds.center - new Vector3(bc.bounds.extents.x, bc.bounds.extents.y + 0.1f), Vector2.right * (bc.bounds.extents.x + groundRaycastLength), Color.red);

        canCornerCorrect = Physics2D.Raycast(transform.position + edgeRaycastOffset, Vector2.up, topRaycastLength, groundLayer) && !Physics2D.Raycast(transform.position + innerRaycastOffset, Vector2.up, topRaycastLength, groundLayer) || Physics2D.Raycast(transform.position - edgeRaycastOffset, Vector2.up, topRaycastLength, groundLayer) && !Physics2D.Raycast(transform.position - innerRaycastOffset, Vector2.up, topRaycastLength, groundLayer);
    }

    /*
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position + edgeRaycastOffset, transform.position + edgeRaycastOffset + Vector3.up * topRaycastLength);
        Gizmos.DrawLine(transform.position - edgeRaycastOffset, transform.position - edgeRaycastOffset + Vector3.up * topRaycastLength);
        Gizmos.DrawLine(transform.position + innerRaycastOffset, transform.position + innerRaycastOffset + Vector3.up * topRaycastLength);
        Gizmos.DrawLine(transform.position - innerRaycastOffset, transform.position - innerRaycastOffset + Vector3.up * topRaycastLength);

        Gizmos.DrawLine(transform.position - innerRaycastOffset + Vector3.up * topRaycastLength, transform.position - innerRaycastOffset + Vector3.up * topRaycastLength + Vector3.left * topRaycastLength);
        Gizmos.DrawLine(transform.position + innerRaycastOffset + Vector3.up * topRaycastLength, transform.position + innerRaycastOffset + Vector3.up * topRaycastLength + Vector3.left * topRaycastLength);
    }
    */

    public void Die()
    {
        rb.velocity = Vector2.zero;
        canMove = false;
        anim.SetTrigger("Dead");
    }
}
