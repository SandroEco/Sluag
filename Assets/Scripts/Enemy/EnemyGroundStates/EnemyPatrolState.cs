using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : EnemyBaseState
{
    private float speed = 3;
    private bool mustFlip;
    private bool inAir;
    public float oldPos = 0.0f;
    private float castDist = 5;
    private float timer = 1f;

    public override void EnterState(EnemyStateManager enemy)
    {
        timer = 1f;
        enemy.mustPatrol = true;
        enemy.anim.SetBool("isChasing", false);

        oldPos = enemy.transform.position.x;
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        if (mustFlip && !inAir || enemy.col.IsTouchingLayers(enemy.groundLayer))
        {
            enemy.mustPatrol = false;
            enemy.transform.localScale = new Vector2(enemy.transform.localScale.x * -1, enemy.transform.localScale.y);
            speed *= -1;
            enemy.mustPatrol = true;
        }

        enemy.rb.velocity = new Vector2(speed * 25 * Time.fixedDeltaTime, enemy.rb.velocity.y);

        if(enemy.transform.position.x > oldPos)
        {
            enemy.isFacingRight = true;
        }
        else if(enemy.transform.position.x < oldPos)
        {
            enemy.isFacingRight = false;
        }
        oldPos = enemy.transform.position.x;

        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            if (enemy.isFacingRight)
            {
                Debug.DrawRay(enemy.transform.position, Vector3.right * castDist, Color.green);
                if (Physics2D.Raycast(enemy.transform.position, Vector3.right, castDist, enemy.playerLayer))
                {
                    enemy.SwitchState(enemy.FoundState);
                }
            }
            else if (!enemy.isFacingRight)
            {
                Debug.DrawRay(enemy.transform.position, Vector3.right * -castDist, Color.green);
                if (Physics2D.Raycast(enemy.transform.position, Vector3.right, -castDist, enemy.playerLayer))
                {
                    enemy.SwitchState(enemy.FoundState);
                }
            }
        }
    }

    public override void FixedUpdateState(EnemyStateManager enemy)
    {
        if (enemy.mustPatrol)
        {
            mustFlip = !Physics2D.OverlapCircle(enemy.groundCheckPos.position, 0.1f, enemy.groundLayer);
            inAir = !Physics2D.OverlapCircle(enemy.airCheckPos.position, 0.1f, enemy.groundLayer);
        }
    }

    public override void OnCollisionEnter2D(EnemyStateManager enemy, Collision2D other)
    {

    }
    public override void OnTriggerEnter2D(EnemyStateManager enemy, Collider2D other)
    {
        if (other.tag == "Hit" || other.tag == "Hit 2")
        {
            enemy.player = GameObject.FindGameObjectWithTag("Player").transform;
            if (enemy.player.transform.position.x > enemy.transform.position.x)
            {
                if (!enemy.isFacingRight)
                {
                    enemy.mustPatrol = false;
                    enemy.transform.localScale = new Vector2(enemy.transform.localScale.x * -1, enemy.transform.localScale.y);
                    speed *= -1;
                    enemy.mustPatrol = true;
                }
            }
            else if (enemy.player.transform.position.x < enemy.transform.position.x)
            {
                if (enemy.isFacingRight)
                {
                    enemy.mustPatrol = false;
                    enemy.transform.localScale = new Vector2(enemy.transform.localScale.x * -1, enemy.transform.localScale.y);
                    speed *= -1;
                    enemy.mustPatrol = true;
                }
            }
            enemy.SwitchState(enemy.HurtState);
        }


        if (other.tag == "InstantDeath")
        {
            enemy.SwitchState(enemy.DeadState);
        }
    }

    public override void LateUpdateState(EnemyStateManager enemy)
    {

    }
}
