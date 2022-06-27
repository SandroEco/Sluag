using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtState : EnemyBaseState
{
    float knockbackForce = 20;
    bool isGrounded;
    float timer = 0.2f;

    public override void EnterState(EnemyStateManager enemy)
    {
        enemy.health -= 1;
        if(enemy.health <= 0)
        {
            enemy.SwitchState(enemy.DeadState);
        }
        else
        {
            timer = 0.2f;
            enemy.anim.SetBool("Hurt", true);
            Vector2 difference = (enemy.transform.position - enemy.player.transform.position).normalized;
            Vector2 force = Vector2.down * difference * knockbackForce;
            enemy.rb.AddForce(force, ForceMode2D.Impulse);
        }
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            isGrounded = Physics2D.OverlapCircle(enemy.airCheckPos.position, 0.1f, enemy.groundLayer);
            if (isGrounded)
            {
                enemy.anim.SetBool("Hurt", false);
                enemy.SwitchState(enemy.PatrolState);
            }
        }
    }

    public override void FixedUpdateState(EnemyStateManager enemy)
    {

    }

    public override void OnCollisionEnter2D(EnemyStateManager enemy, Collision2D other)
    {

    }

    public override void OnTriggerEnter2D(EnemyStateManager enemy, Collider2D other)
    {

    }

    public override void LateUpdateState(EnemyStateManager enemy)
    {

    }
}
