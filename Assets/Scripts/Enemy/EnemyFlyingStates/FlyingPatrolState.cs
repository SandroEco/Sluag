using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingPatrolState : FlyingEnemyBaseState
{
    public override void EnterState(FlyEnemyStateManager enemy)
    {

    }

    public override void UpdateState(FlyEnemyStateManager enemy)
    {

    }

    public override void FixedUpdateState(FlyEnemyStateManager enemy)
    {

    }

    public override void OnCollisionEnter2D(FlyEnemyStateManager enemy, Collision2D other)
    {

    }
    public override void OnTriggerEnter2D(FlyEnemyStateManager enemy, Collider2D other)
    {
        if(other.tag == "Player")
        {
            enemy.SwitchState(enemy.AttackState);
        }
    }
    public override void OnTriggerExit2D(FlyEnemyStateManager enemy, Collider2D other)
    {

    }

    public override void LateUpdateState(FlyEnemyStateManager enemy)
    {

    }
}
