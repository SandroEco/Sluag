using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSecondState : BossBaseState
{

    private float timer = 1.2f;

    public override void EnterState(BossStateManager boss)
    {
        if(boss.count == 2)
        {
            boss.SwitchState(boss.ThirdState);
        }
        else
        {
            boss.anim.SetBool("Attack", true);
            boss.count++;
            timer = 1.2f;
        }
    }

    public override void UpdateState(BossStateManager boss)
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            boss.SwitchState(boss.SecondState);
        }

        if (boss.health <= 0)
        {
            EnemyStateManager.Destroy(boss.gameObject);
        }
    }

    public override void FixedUpdateState(BossStateManager boss)
    {

    }

    public override void LateUpdateState(BossStateManager boss)
    {

    }

    public override void OnCollisionEnter2D(BossStateManager boss, Collision2D other)
    {

    }

    public override void OnTriggerEnter2D(BossStateManager boss, Collider2D other)
    {
        if (other.tag == "Stone")
        {
            boss.health -= 1;
        }
    }
}
