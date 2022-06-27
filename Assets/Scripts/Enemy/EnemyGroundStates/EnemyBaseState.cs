using UnityEngine;

public abstract class EnemyBaseState
{
    public abstract void EnterState(EnemyStateManager enemy);

    public abstract void UpdateState(EnemyStateManager enemy);

    public abstract void FixedUpdateState(EnemyStateManager enemy);

    public abstract void LateUpdateState(EnemyStateManager enemy);

    public abstract void OnCollisionEnter2D(EnemyStateManager enemy, Collision2D other);

    public abstract void OnTriggerEnter2D(EnemyStateManager enemy, Collider2D other);
}
