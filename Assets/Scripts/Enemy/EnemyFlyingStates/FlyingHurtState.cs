using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingHurtState : FlyingEnemyBaseState
{
    float knockbackForce = 10;
    float timer = 1f;

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
            timer = 0.2f;
            enemy.anim.SetTrigger("Hurt");
            Vector2 difference = (enemy.transform.position - enemy.player.transform.position).normalized;
            Vector2 force = Vector2.down * difference * knockbackForce;
            enemy.rb.AddForce(force, ForceMode2D.Impulse);
        }
    }

    public override void UpdateState(FlyEnemyStateManager enemy)
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
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

    }

    public override void OnTriggerExit2D(FlyEnemyStateManager enemy, Collider2D other)
    {

    }
}

