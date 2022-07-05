using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    public static EnemyStateManager instance;

    [Header("Components")]
    public Rigidbody2D rb;
    public Transform groundCheckPos;
    public Transform airCheckPos;
    public Transform player;
    public Collider2D col;
    public Animator anim;
    public LayerMask groundLayer;
    public LayerMask playerLayer;
    public GameObject playerFound;
    public SFXController sfx;

    public bool mustPatrol;
    public bool animationEnded;

    [Header("Stats")]
    public int health;
    public bool isFacingRight;
    public float knockbackForce;

    [Header("Drop")]
    public int numOfGold;
    public GameObject Gold;
    public GameObject drop;

    EnemyBaseState currentState;
    public EnemyPatrolState PatrolState = new EnemyPatrolState();
    public EnemyChaseState ChaseState = new EnemyChaseState();
    public EnemyDeadState DeadState = new EnemyDeadState();
    public EnemyHurtState HurtState = new EnemyHurtState();
    public EnemyFoundState FoundState = new EnemyFoundState();

    void Start()
    {
        currentState = PatrolState;

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

    public void SwitchState(EnemyBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
}
