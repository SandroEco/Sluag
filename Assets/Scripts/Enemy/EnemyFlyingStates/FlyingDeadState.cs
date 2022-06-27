using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingDeadState : FlyingEnemyBaseState
{
    float deathTimer = 0.7f;
    bool once;

    public override void EnterState(FlyEnemyStateManager enemy)
    {
        once = true;
        enemy.anim.SetTrigger("Dead");
        enemy.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
    }

    public override void UpdateState(FlyEnemyStateManager enemy)
    {
        if (deathTimer > 0)
        {
            deathTimer -= Time.deltaTime;
        }
        else
        {
            if (once)
            {
                for (int i = 0; i < enemy.numOfGold; i++)
                {
                    enemy.Gold = FlyEnemyStateManager.Instantiate(enemy.Gold, enemy.transform.position, Quaternion.identity);
                }
                /*
                if (!drop)
                {
                    return;
                }
                else
                {
                    Instantiate(drop, transform.position, Quaternion.identity);
                }
                */
            }
            once = false;
            FlyEnemyStateManager.Destroy(enemy.gameObject);
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
