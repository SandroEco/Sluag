using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossThirdState : BossBaseState
{
    private float timer = 4;
    public override void EnterState(BossStateManager boss)
    {
        timer = 4;
        boss.anim.SetBool("Attack", false);
        boss.anim.SetTrigger("Throw");
        boss.count = 0;
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

        if(boss.health <= 0)
        {
            boss.transform.Find("Dialog1").GetComponent<DialogTrigger>().StartDialog();
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
