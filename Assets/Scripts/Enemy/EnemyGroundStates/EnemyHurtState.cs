using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtState : EnemyBaseState
{
    bool isGrounded;
    float knockbackTime = 0.5f;

    public override void EnterState(EnemyStateManager enemy)
    {
        enemy.health -= 1;
        if(enemy.health <= 0)
        {
            enemy.SwitchState(enemy.DeadState);
        }
        else
        {
            knockbackTime = 0.5f;
            enemy.anim.SetBool("Hurt", true);
            Vector2 difference = (enemy.transform.position - enemy.player.transform.position).normalized;
            Vector2 force = difference * enemy.knockbackForce;
            enemy.rb.AddForce(force, ForceMode2D.Impulse);
        }
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        if (knockbackTime > 0)
        {
            knockbackTime -= Time.deltaTime;
        }
        else
        {
            enemy.anim.SetBool("Hurt", false);
            enemy.SwitchState(enemy.PatrolState);
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
        if(other.tag == "Hit 2")
        {
            enemy.health -= 1;
            if (enemy.health <= 0)
            {
                enemy.anim.SetBool("Hurt", false);
                enemy.SwitchState(enemy.DeadState);
            }
            else
            {
                knockbackTime = 0.7f;
                enemy.anim.SetBool("Hurt", true);
                Vector2 difference = (enemy.transform.position - enemy.player.transform.position).normalized;
                Vector2 force = difference * enemy.knockbackForce * 1.5f;
                enemy.rb.AddForce(force, ForceMode2D.Impulse);
            }
        }
    }

    public override void LateUpdateState(EnemyStateManager enemy)
    {

    }
}
