using UnityEngine;

public abstract class BossBaseState
{
    public abstract void EnterState(BossStateManager boss);

    public abstract void UpdateState(BossStateManager boss);

    public abstract void FixedUpdateState(BossStateManager boss);

    public abstract void LateUpdateState(BossStateManager boss);

    public abstract void OnCollisionEnter2D(BossStateManager boss, Collision2D other);

    public abstract void OnTriggerEnter2D(BossStateManager boss, Collider2D other);
}
