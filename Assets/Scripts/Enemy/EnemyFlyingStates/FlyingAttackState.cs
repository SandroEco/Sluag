using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingAttackState : FlyingEnemyBaseState
{
    bool reachedEndOfPath = false;
    private float range = 10;

    public override void EnterState(FlyEnemyStateManager enemy)
    {
        enemy.target = GameObject.FindWithTag("Player").transform;
        enemy.InvokeRepeating("UpdatePath", 0f, .5f);
    }

    public override void UpdateState(FlyEnemyStateManager enemy)
    {
        Collider2D collider = Physics2D.OverlapCircle(enemy.transform.position, range, enemy.playerLayer);
        if(collider == null)
        {
            enemy.SwitchState(enemy.PatrolState);
        }
        else
        {
            return;
        }
    }

    public override void FixedUpdateState(FlyEnemyStateManager enemy)
    {
        if (enemy.path == null)
        {
            return;
        }

        if (enemy.currentWaypoint >= enemy.path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)enemy.path.vectorPath[enemy.currentWaypoint] - enemy.rb.position).normalized;
        Vector2 force = direction * enemy.speed * Time.deltaTime;

        enemy.rb.AddForce(force);

        float distance = Vector2.Distance(enemy.rb.position, enemy.path.vectorPath[enemy.currentWaypoint]);

        if(distance < enemy.nextWaypointDistance)
        {
            enemy.currentWaypoint++;
        }

        if(enemy.rb.velocity.x >= 0.01f)
        {
            enemy.enemySprite.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if(enemy.rb.velocity.x <= 0.01f)
        {
            enemy.enemySprite.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    public override void OnCollisionEnter2D(FlyEnemyStateManager enemy, Collision2D other)
    {

    }
    public override void OnTriggerEnter2D(FlyEnemyStateManager enemy, Collider2D other)
    {
        if (other.gameObject.tag == "Hit" || other.gameObject.tag == "Hit 2")
        {
            enemy.SwitchState(enemy.HurtState);
        }
    }
    public override void OnTriggerExit2D(FlyEnemyStateManager enemy, Collider2D other)
    {

    }

    public override void LateUpdateState(FlyEnemyStateManager enemy)
    {

    }
}
