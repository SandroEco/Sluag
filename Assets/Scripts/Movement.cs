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

    [Header("Layer Masks")]
    [SerializeField]private LayerMask groundLayer;

    [Header("Movement Variables")]
    public float movementAcceleration;
    public float maxMoveSpeed;
    public float movementDeceleration;
    private float horizontalDirection;
    private bool changingDirection => (rb.velocity.x > 0f && horizontalDirection < 0f) || (rb.velocity.x < 0f && horizontalDirection > 0f);
    private bool isFacingRight = true;
    public bool canWalk;
       
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

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        #region Movement
        horizontalDirection = GetInput().x;
        #endregion

        #region Jumping
        if (canJump)
        {
            jumpBufferCounter = 0f;
            anim.SetTrigger("takeOf");
            Jump();
        }

        if (isGrounded)
        {
            anim.SetBool("isJumping", false);
            ApplyGroundLinearDrag();
        }
        else
        {
            anim.SetBool("isJumping", true);
            ApplyAirLinearDrag();
            FallMultiplier();
        }
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
        MoveCharacter();
        CheckCollisions();
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
            rb.AddForce(new Vector2(horizontalDirection, 0f) * movementAcceleration);
            anim.SetFloat("speed", Mathf.Abs(horizontalDirection));

        if(isFacingRight && horizontalDirection < 0 || !isFacingRight && horizontalDirection > 0)
        {
            Flip();
        }

        if(Mathf.Abs(rb.velocity.x) > maxMoveSpeed)
        {
            anim.SetBool("MaxSpeedReached", true);
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxMoveSpeed, rb.velocity.y);
        }
        else
        {
            anim.SetBool("MaxSpeedReached", false);
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    }

    private void ApplyAirLinearDrag()
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

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void FallMultiplier()
    {
        if(rb.velocity.y < 0)
        {
            rb.gravityScale = fallMultiplier;
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

    private void CheckCollisions()
    {
        isGrounded = Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, groundRaycastLength, Vector2.down, groundRaycastLength, groundLayer);
        Debug.DrawRay(bc.bounds.center + new Vector3(bc.bounds.extents.x, 0), Vector2.down * (bc.bounds.extents.y + groundRaycastLength), Color.red);
        Debug.DrawRay(bc.bounds.center - new Vector3(bc.bounds.extents.x, 0), Vector2.down * (bc.bounds.extents.y + groundRaycastLength), Color.red);
        Debug.DrawRay(bc.bounds.center - new Vector3(bc.bounds.extents.x, bc.bounds.extents.y + 0.1f), Vector2.right * (bc.bounds.extents.x + groundRaycastLength), Color.red);
    }
}
