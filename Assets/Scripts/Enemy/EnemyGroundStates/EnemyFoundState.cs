using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFoundState : EnemyBaseState
{
    private float timer = 0.7f;
    public override void EnterState(EnemyStateManager enemy)
    {
        timer = 0.7f;
        enemy.anim.SetTrigger("Surprised");
        enemy.playerFound.SetActive(true);
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            enemy.playerFound.SetActive(false);
            enemy.SwitchState(enemy.ChaseState);
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
        if (other.tag == "Hit" || other.tag == "Hit 2")
        {
            if (enemy.health <= 0)
            {
                enemy.SwitchState(enemy.DeadState);
            }
            else
            {
                enemy.player = GameObject.FindGameObjectWithTag("Player").transform;
                enemy.SwitchState(enemy.HurtState);
            }
        }
    }

    public override void LateUpdateState(EnemyStateManager enemy)
    {

    }
}
