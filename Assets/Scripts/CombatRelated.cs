using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatRelated : MonoBehaviour
{
    [Header("Components")]
    private Animator anim;
    private Rigidbody2D rb;

    public static float damage;
    public float timeBtwAttack;
    public float cooldown;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(timeBtwAttack <= 0)
        {
            if (Input.GetKeyDown("q"))
            {
                rb.velocity = Vector3.zero;
                anim.SetBool("isHitting", true);
                damage = 5f;

                if(timeBtwAttack <= 0f)
                {
                    timeBtwAttack = cooldown;
                }
            }
        }
        else
        {
            anim.SetBool("isHitting", false);
            timeBtwAttack -= Time.deltaTime;
        }
    }
}
