using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFirstState : BossBaseState
{
    float switchToSecondState = 3;

    public override void EnterState(BossStateManager boss)
    {
        switchToSecondState = 3;
    }

    public override void UpdateState(BossStateManager boss)
    {

        if(boss.bossFightTrigger.startBossFight == true)
        {
            boss.transform.localScale = new Vector2(-1, 1);
            //, changeState, cameraPosition

            if (switchToSecondState > 0)
            {
                switchToSecondState -= Time.deltaTime;
            }
            else
            {
                boss.SwitchState(boss.SecondState);
            }
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

    }
}
