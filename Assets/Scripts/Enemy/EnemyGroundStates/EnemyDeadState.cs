using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadState : EnemyBaseState
{
    float deathTimer = 1.3f;
    bool once;
    public override void EnterState(EnemyStateManager enemy)
    {
        once = true;
        enemy.anim.SetTrigger("Dead");
        enemy.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
    }

    public override void UpdateState(EnemyStateManager enemy)
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
                    enemy.Gold = EnemyStateManager.Instantiate(enemy.Gold, enemy.transform.position, Quaternion.identity);
                }
            }
            once = false;

            if (!enemy.drop)
            {
                EnemyStateManager.Destroy(enemy.gameObject);
            }
            else
            {
                enemy.drop = EnemyStateManager.Instantiate(enemy.drop, enemy.transform.position, Quaternion.identity);
                EnemyStateManager.Destroy(enemy.gameObject);
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
