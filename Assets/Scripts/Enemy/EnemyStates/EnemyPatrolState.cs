using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : EnemyBaseState
{
    private float speed = 3;
    private bool mustFlip;
    private bool mustPatrol;
    public float oldPos = 0.0f;
    private bool isFacingRight;
    private float castDist = 5;
    private bool inAir;

    public override void EnterState(EnemyStateManager enemy)
    {
        oldPos = enemy.transform.position.x;
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        if (mustFlip && !inAir || enemy.col.IsTouchingLayers(enemy.groundLayer))
        {
            mustPatrol = false;
            enemy.transform.localScale = new Vector2(enemy.transform.localScale.x * -1, enemy.transform.localScale.y);
            speed *= -1;
            mustPatrol = true;
        }

        enemy.rb.velocity = new Vector2(speed * 25 * Time.fixedDeltaTime, enemy.rb.velocity.y);

        if(enemy.transform.position.x > oldPos)
        {
            isFacingRight = true;
        }
        else if(enemy.transform.position.x < oldPos)
        {
            isFacingRight = false;
        }
        oldPos = enemy.transform.position.x;

        if (isFacingRight)
        {
            Debug.DrawRay(enemy.transform.position, Vector3.right * castDist, Color.green);
            if (Physics2D.Raycast(enemy.transform.position, Vector3.right, castDist, enemy.playerLayer))
            {
                enemy.SwitchState(enemy.ChaseState);
            }
        }
        else if (!isFacingRight)
        {
            Debug.DrawRay(enemy.transform.position, Vector3.right * -castDist, Color.green);
            if (Physics2D.Raycast(enemy.transform.position, Vector3.right, -castDist, enemy.playerLayer))
            {
                enemy.SwitchState(enemy.ChaseState);
            }
        }
    }

    public override void FixedUpdateState(EnemyStateManager enemy)
    {
        if (mustPatrol)
        {
            mustFlip = !Physics2D.OverlapCircle(enemy.groundCheckPos.position, 0.1f, enemy.groundLayer);
            inAir = !Physics2D.OverlapCircle(enemy.airCheckPos.position, 0.1f, enemy.groundLayer);

        }
    }

    public override void OnCollisionEnter2D(EnemyStateManager enemy, Collision2D other)
    {

    }
}
