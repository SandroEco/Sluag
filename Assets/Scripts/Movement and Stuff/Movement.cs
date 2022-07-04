using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public static Movement Instance { get; private set; }
    
    [Header("Components")]
    public Rigidbody2D rb;
    private PolygonCollider2D bc;
    private Animator anim;
    private Acceleration accelerationScript;
    private WallJumping wallJumping;
    public Camera cam;
    Animator camAnim;
    
    [Header("Layer Masks")]
    [SerializeField]private LayerMask groundLayer;

    [Header("Movement Variables")]
    public float movementAcceleration;
    public float maxMoveSpeed;
    public float movementDeceleration;
    public float horizontalDirection;
    private bool changingDirection => (rb.velocity.x > 0f && horizontalDirection < 0f) || (rb.velocity.x < 0f && horizontalDirection > 0f);
    public bool isFacingRight = true;
    public bool canMove;
       
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

    [Header("Others")]
    public float strength;
    public bool isDying;
    public bool isPlayingAnimation;

    [Header("NPC Stuff")]
    public int readLetter;
    public int talkedAboutLetter;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<PolygonCollider2D>();
        anim = GetComponent<Animator>();
        accelerationScript = GetComponent<Acceleration>();
        camAnim = cam.GetComponent<Animator>();
        isPlayingAnimation = false;

        if (SaveManager.instance.hasLoaded)
        {
            readLetter = SaveManager.instance.activeSave.readLetter;
            talkedAboutLetter = SaveManager.instance.activeSave.talkedAboutLetter;
        }
        else
        {
            SaveManager.instance.activeSave.readLetter = readLetter;
            SaveManager.instance.activeSave.talkedAboutLetter = talkedAboutLetter;
        }

        Physics2D.IgnoreLayerCollision(2, 7, true);
    }

    void Update()
    {
        if(DialogManager.isActive == true)
        {
            horizontalDirection = 0;
            return;
        }

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

        if (isDying)
        {
            canMove = false;
            rb.velocity = Vector2.zero;
        }
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
        if (canJump && canMove)
        {
            jumpBufferCounter = 0f;
            anim.SetTrigger("takeOf");
            Jump();
        }
        #endregion
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
        if(wallJumping = GetComponent<WallJumping>())
        {
            wallJumping.wallJumpDirection *= -1f;
        }
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

    private void CheckCollisions()
    {
        isGrounded = Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, groundRaycastLength, Vector2.down, groundRaycastLength, groundLayer);
        Debug.DrawRay(bc.bounds.center + new Vector3(bc.bounds.extents.x, 0), Vector2.down * (bc.bounds.extents.y + groundRaycastLength), Color.red);
        Debug.DrawRay(bc.bounds.center - new Vector3(bc.bounds.extents.x, 0), Vector2.down * (bc.bounds.extents.y + groundRaycastLength), Color.red);
        Debug.DrawRay(bc.bounds.center - new Vector3(bc.bounds.extents.x, bc.bounds.extents.y + 0.1f), Vector2.right * (bc.bounds.extents.x + groundRaycastLength), Color.red);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Key")
        {
            canMove = false;
            isPlayingAnimation = true;
            StartCoroutine(WaitForAnim());
        }
    }

    private IEnumerator WaitForAnim()
    {
        anim.SetTrigger("Win");
        canMove = false;
        yield return new WaitForSeconds(1.7f);
        isPlayingAnimation = false;
        canMove = true;
    }

    public void Die()
    {
        isDying = true;
        anim.SetTrigger("Dead");
        camAnim.SetTrigger("Dead");
    }

    public void Damaged()
    {
        anim.SetTrigger("Damaged");
    }
}
