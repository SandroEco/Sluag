using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearEnemy : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody2D rb;
    public Transform groundCheckPos;
    public Transform airCheckPos;
    public Animator anim;
    public PolygonCollider2D bodyCollider;
    public BoxCollider2D antlers;
    public EnemyHealth enemyHealth;
    private Transform target;
    public HealthAll healthAllScript;

    [Header("Layermasks")]
    public LayerMask groundLayer;
    public LayerMask playerLayer;

    [Header("Movement")]
    public float walkSpeed;
    public float huntSpeed;
    public bool mustPatrol;
    public bool mustFlip;
    public bool inAir;

    [Header("RaycastStuff")]
    public float maxDistance;
    private float oldPos = 0.0f;
    public bool isFacingRight;
    public bool playerFound;
    public Vector2 targetDirection;
    private bool checkForPlayer;

    private bool once;
    enum EnemyState
    {
        Patrol,
        Hunt,
        Stunned,
        Dead
    }

    EnemyState currentState = EnemyState.Patrol;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        healthAllScript = GameObject.FindObjectOfType<HealthAll>();

        checkForPlayer = true;
        once = true;
        mustPatrol = true;
        oldPos = transform.position.x;

    }

    private void OnEnable()
    {
        currentState = EnemyState.Patrol;
        CheckForPlayer();
        enemyHealth.isKnockbacked = false;
        playerFound = false;
    }

    void Update()
    {
        switch (currentState)
        {
            case EnemyState.Patrol:
                Patrol();
                CheckForPlayer();
                break;
            case EnemyState.Hunt:
                CheckForPlayer();
                RunTowardsPlayer();
                break;
            case EnemyState.Stunned:
                Stunned();
                break;
            case EnemyState.Dead:
                break;
        }

        if(enemyHealth.HP <= 0)
        {
            currentState = EnemyState.Dead;
        }
    }

    void Patrol()
    {
        mustFlip = !Physics2D.OverlapCircle(groundCheckPos.position, 0.1f, groundLayer);
        inAir = !Physics2D.OverlapCircle(airCheckPos.position, 0.1f, groundLayer);

        if (mustFlip && !inAir || bodyCollider.IsTouchingLayers(groundLayer))
        {
            Flip();
        }
        anim.SetBool("MaxSpeedReached", false);
        if (!enemyHealth.isKnockbacked /*&& !isStunned/ */)
        {
            rb.velocity = new Vector2(walkSpeed * 25 * Time.fixedDeltaTime, rb.velocity.y);
        }
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
        if (checkForPlayer)
        {
            if (transform.position.x > oldPos)
            {
                isFacingRight = true;
            }
            else if (transform.position.x < oldPos)
            {
                isFacingRight = false;
            }
            oldPos = transform.position.x;

            if (isFacingRight)
            {
                Debug.DrawRay(transform.position, Vector3.right * maxDistance, Color.green);

                if (Physics2D.Raycast(transform.position, Vector3.right, maxDistance, playerLayer))
                {
                    playerFound = true;
                    Debug.DrawRay(transform.position, Vector3.up - Vector3.left * maxDistance, Color.red);
                    //Debug.DrawRay(transform.position, Vector3.right * maxDistance, Color.red);
                    currentState = EnemyState.Hunt;
                    targetDirection = new Vector2(target.position.x, transform.position.y);
                }
                else
                {
                    if (once && playerFound)
                    {
                        StartCoroutine(Timer());
                    }
                    playerFound = false;
                }

            }

            if (!isFacingRight)
            {
                Debug.DrawRay(transform.position, Vector3.left * maxDistance, Color.green);

                if (Physics2D.Raycast(transform.position, Vector3.left, maxDistance, playerLayer))
                {
                    playerFound = true;
                    Debug.DrawRay(transform.position, Vector3.left * maxDistance, Color.red);
                    //Debug.DrawRay(transform.position, Vector3.up - Vector3.right * maxDistance, Color.red);
                    currentState = EnemyState.Hunt;
                    targetDirection = new Vector2(target.position.x, transform.position.y);
                }
                else
                {
                    if (once && playerFound)
                    {
                        StartCoroutine(Timer());
                    }
                    playerFound = false;
                }
            }
        }
        
    }

    void RunTowardsPlayer()
    {
        anim.SetBool("MaxSpeedReached", true);
        mustPatrol = false;

        if (isFacingRight && !enemyHealth.isKnockbacked)
        {
            transform.position = transform.position + (transform.right * huntSpeed * Time.deltaTime);
        }
        if (!isFacingRight && !enemyHealth.isKnockbacked)
        {
            transform.position = transform.position + (-transform.right * huntSpeed * Time.deltaTime);
        }

        if (antlers.IsTouchingLayers(groundLayer))
        {
            anim.SetBool("MaxSpeedReached", false);
            Bonk();
        }
    }

    private IEnumerator Timer()
    {
        once = false;
        yield return new WaitForSeconds(1);
        if(!playerFound)
        {
            currentState = EnemyState.Patrol;
        }
        once = true;
    }

    private void Bonk()
    {
        checkForPlayer = false;
        enemyHealth.isKnockbacked = true;
        anim.SetBool("isStunned", true);
        if (isFacingRight)
        {
            rb.AddForce((Vector3.up - Vector3.right) * 0.2f, ForceMode2D.Impulse);
        }
        if (!isFacingRight)
        {
            rb.AddForce((Vector3.up - Vector3.left) * 0.2f, ForceMode2D.Impulse);
        }
        StartCoroutine(BonkTimer());
        StartCoroutine(Stunned());
    }

    private IEnumerator BonkTimer()
    {
        playerFound = true;
        yield return new WaitForSeconds(1);
        rb.velocity = Vector3.zero;
        currentState = EnemyState.Stunned;
    }
    private IEnumerator Stunned()
    {
        playerFound = true;
        //isStunned = true;
        yield return new WaitForSeconds(3.5f);
        //isStunned = false;
        anim.SetBool("MaxSpeedReached", false);
        anim.SetBool("isStunned", false);
        enemyHealth.isKnockbacked = false;
        checkForPlayer = true;
        currentState = EnemyState.Patrol;
    }
}
