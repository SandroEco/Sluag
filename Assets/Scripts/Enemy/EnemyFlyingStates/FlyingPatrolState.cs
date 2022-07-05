using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingPatrolState : FlyingEnemyBaseState
{

    private float range = 10;

    public override void EnterState(FlyEnemyStateManager enemy)
    {
        
    }

    public override void UpdateState(FlyEnemyStateManager enemy)
    {
        Collider2D collider = Physics2D.OverlapCircle(enemy.transform.position, range, enemy.playerLayer);
        if(collider != null)
        {
            enemy.SwitchState(enemy.AttackState);
        }
        else
        {
            return;
        }
    }

    public override void FixedUpdateState(FlyEnemyStateManager enemy)
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

    public override void LateUpdateState(FlyEnemyStateManager enemy)
    {

    }
   
}
