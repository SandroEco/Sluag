 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatRelated : MonoBehaviour
{
    [Header("Components")]
    private Animator anim;
    private Rigidbody2D rb;
    public HealthAll healthAllScript;
    private Movement movement;
    public Invulnerability invulnerability;

    [Header("Knockback")]
    public float knockback;
    public float knockbackTime;
    public bool isKnockbacked;

    public static float damage;
    public float timeBtwAttack;
    public float cooldown;
    public bool isAttacking;

    private void Start()
    {
        isKnockbacked = false;
        healthAllScript = GameObject.FindObjectOfType<HealthAll>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        movement = GetComponent<Movement>();
        invulnerability = GetComponent<Invulnerability>();
    }

    private void Update()
    {

        if (timeBtwAttack <= 0)
        {
            isAttacking = false;
            if (Input.GetButtonDown("Punch"))
            {
                isAttacking = true;
                rb.velocity = Vector3.zero;
                anim.SetTrigger("isHitting");
                damage = 5f;

                if(timeBtwAttack <= 0f)
                {
                    timeBtwAttack = cooldown;
                }
            }
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }

        if (isAttacking)
        {
            movement.canMove = false;
        }
        else if(!isAttacking && Door.canInteract)
        {
            movement.canMove = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        rb = GetComponent<Rigidbody2D>();
        if (other.tag == "BearAntlers")
        {
            CinemachineShake.ShakeInstance.ShakeCamera(4f, .1f);
            isKnockbacked = true;
            Vector2 difference = (transform.position - other.transform.position);
            difference.y = 1f;
            Vector2 force = difference * knockback;
            rb.AddForce(force, ForceMode2D.Impulse);
            healthAllScript.TakeDamage(1);
            StartCoroutine(KnockbackCounter());
        }

        /*
        rb = GetComponent<Rigidbody2D>();
        if (other.tag == "Demon")
        {
            CinemachineShake.ShakeInstance.ShakeCamera(5f, .15f);
            isKnockbacked = true;
            Vector2 difference = (transform.position - other.transform.position);
            difference.y = 0.4f;
            Vector2 force = difference * knockback;
            rb.AddForce(force, ForceMode2D.Impulse);
            StartCoroutine(KnockbackCounter());
            healthAllScript.TakeDamage(1);
        }
        */
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        rb = GetComponent<Rigidbody2D>();
        if (other.gameObject.tag == "Demon")
        {
            healthAllScript.TakeDamage(1);
            CinemachineShake.ShakeInstance.ShakeCamera(5f, .15f);
            isKnockbacked = true;
            Vector2 difference = (transform.position - other.transform.position);
            difference.y = 0.4f;
            Vector2 force = difference * knockback;
            rb.AddForce(force, ForceMode2D.Impulse);
            StartCoroutine(KnockbackCounter());
        }

        if (other.gameObject.tag == "Stone")
        {
            healthAllScript.TakeDamage(1);
            CinemachineShake.ShakeInstance.ShakeCamera(5f, .15f);
        }
    }

    private IEnumerator KnockbackCounter()
    {
        yield return new WaitForSeconds(knockbackTime);
        isKnockbacked = false;
    }

    public void PunchPush()
    {
        if (movement.isFacingRight)
        {
            rb.AddForce(new Vector2(2, 0), ForceMode2D.Impulse);
        }
        else if (!movement.isFacingRight)
        {
            rb.AddForce(new Vector2(-2, 0), ForceMode2D.Impulse);

        }
    }
}
