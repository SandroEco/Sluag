using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{
    private float speed = 5;
    private float runDur = 3;
    public override void EnterState(EnemyStateManager enemy)
    {
        enemy.anim.SetBool("isChasing", true);
        runDur = 3;
        enemy.sfx.Tier1DemonSnarl();
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        if(runDur > 0)
        {
            runDur -= Time.deltaTime;
        }
        else
        {
            enemy.SwitchState(enemy.PatrolState);
        }

        if (enemy.isFacingRight)
        {
            enemy.transform.position = enemy.transform.position + (enemy.transform.right * speed * Time.deltaTime);
        }
        else if (!enemy.isFacingRight)
        {
            enemy.transform.position = enemy.transform.position + (-enemy.transform.right * speed * Time.deltaTime);
        }

        if (enemy.col.IsTouchingLayers(enemy.groundLayer))
        {
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
        if (other.tag == "Hit" || other.tag == "Hit 2")
        {
            enemy.player = GameObject.FindGameObjectWithTag("Player").transform;
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
