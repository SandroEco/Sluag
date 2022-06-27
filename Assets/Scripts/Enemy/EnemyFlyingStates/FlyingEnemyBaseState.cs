using UnityEngine;

public abstract class FlyingEnemyBaseState
{
    public abstract void EnterState(FlyEnemyStateManager enemy);

    public abstract void UpdateState(FlyEnemyStateManager enemy);

    public abstract void FixedUpdateState(FlyEnemyStateManager enemy);

    public abstract void LateUpdateState(FlyEnemyStateManager enemy);

    public abstract void OnCollisionEnter2D(FlyEnemyStateManager enemy, Collision2D other);

    public abstract void OnTriggerEnter2D(FlyEnemyStateManager enemy, Collider2D other);

    public abstract void OnTriggerExit2D(FlyEnemyStateManager enemy, Collider2D other);
}
