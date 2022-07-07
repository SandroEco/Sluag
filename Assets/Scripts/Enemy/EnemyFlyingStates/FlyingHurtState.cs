using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingHurtState : FlyingEnemyBaseState
{
    float knockbackForce = 3;
    float knockbackTime = 0.5f;

    public override void EnterState(FlyEnemyStateManager enemy)
    {
        enemy.player = GameObject.FindWithTag("Player").transform;
        enemy.health -= 1;
        if (enemy.health <= 0)
        {
            enemy.SwitchState(enemy.DeadState);
        }
        else
        {
            knockbackTime = 0.5f;
            enemy.anim.SetTrigger("Hurt");
            Vector2 difference = (enemy.transform.position - enemy.player.transform.position).normalized;
            Vector2 force = difference * knockbackForce;
            enemy.rb.AddForce(force, ForceMode2D.Impulse);
        }
    }

    public override void UpdateState(FlyEnemyStateManager enemy)
    {
        if(knockbackTime > 0)
        {
            knockbackTime -= Time.deltaTime;
        }
        else
        {
            enemy.SwitchState(enemy.AttackState);
        }
    }

    public override void FixedUpdateState(FlyEnemyStateManager enemy)
    {

    }

    public override void LateUpdateState(FlyEnemyStateManager enemy)
    {

    }

    public override void OnCollisionEnter2D(FlyEnemyStateManager enemy, Collision2D other)
    {

    }

    public override void OnTriggerEnter2D(FlyEnemyStateManager enemy, Collider2D other)
    {
        if (other.tag == "Hit 2")
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
                Vector2 force = difference * knockbackForce * 2f;
                enemy.rb.AddForce(force, ForceMode2D.Impulse);
            }
        }
    }

    public override void OnTriggerExit2D(FlyEnemyStateManager enemy, Collider2D other)
    {

    }
}

