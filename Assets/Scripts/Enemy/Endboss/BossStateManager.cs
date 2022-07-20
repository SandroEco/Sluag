using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateManager : MonoBehaviour
{
    public static BossBaseState instance;

    public BossFightTrigger bossFightTrigger;

    BossBaseState currentState;
    public BossFirstState FirstState = new BossFirstState();
    public BossSecondState SecondState = new BossSecondState();
    public BossThirdState ThirdState = new BossThirdState();

    void Start()
    {
        currentState = FirstState;

        currentState.EnterState(this);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        currentState.OnCollisionEnter2D(this, other);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        currentState.OnTriggerEnter2D(this, other);
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    private void FixedUpdate()
    {
        currentState.FixedUpdateState(this);
    }

    private void LateUpdate()
    {
        currentState.LateUpdateState(this);
    }

    public void SwitchState(BossBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
}
