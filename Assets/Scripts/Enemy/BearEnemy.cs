using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearEnemy : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody2D rb;
    public Transform groundCheckPos;
    public Animator anim;
    public PolygonCollider2D bodyCollider;
    public EnemyHealth enemyHealth;
    private Transform target;

    [Header("Layermasks")]
    public LayerMask groundLayer;
    public LayerMask playerLayer;

    [Header("Movement")]
    public float walkSpeed;
    public float huntSpeed;
    public bool mustPatrol;
    private bool mustFlip;

    [Header("RaycastStuff")]
    public float maxDistance;
    private float oldPos = 0.0f;
    private bool isFacingRight;
    public bool playerFound;

    [Header("Damage")]
    public int touchDamage;

    public Vector2 targetDirection;

    private bool once;
    enum EnemyState
    {
        Patrol,
        Hunt
    }

    EnemyState currentState = EnemyState.Patrol;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        once = true;
        mustPatrol = true;
        oldPos = transform.position.x;
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
        }
    }

    private void FixedUpdate()
    {
        
    }

    void Patrol()
    {
        mustFlip = !Physics2D.OverlapCircle(groundCheckPos.position, 0.1f, groundLayer);

        if (mustFlip || bodyCollider.IsTouchingLayers(groundLayer) && !enemyHealth.isKnockbacked)
        {
            Flip();
            Debug.Log("flip");
        }
        anim.SetBool("MaxSpeedReached", false);
        if (!enemyHealth.isKnockbacked)
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
        if(transform.position.x > oldPos)
        {
            isFacingRight = true;
        }
        else if(transform.position.x < oldPos)
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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            var healthComponent = collision.gameObject.GetComponent<PlayerHealth>();
            if (healthComponent != null)
            {
                healthComponent.TakeDamage(touchDamage);
                //dicker Knockback
                Debug.Log("Aua");
            }
        }
    }

    private IEnumerator Timer()
    {
        once = false;
        Debug.Log("startCoroutine");
        yield return new WaitForSeconds(4);
        if(!playerFound)
        {
            currentState = EnemyState.Patrol;
        }
        once = true;
        Debug.Log("endCoroutine");
    }
}
