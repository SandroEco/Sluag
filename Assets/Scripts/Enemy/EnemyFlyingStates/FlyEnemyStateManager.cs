using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FlyEnemyStateManager : MonoBehaviour
{
    public static FlyEnemyStateManager instance;

    [Header("Components")]
    public Seeker seeker;
    public Rigidbody2D rb;
    public Transform enemySprite;
    public BoxCollider2D bc;
    public Animator anim;
    public Transform player;
    public LayerMask playerLayer;

    [Header("Stats")]
    public int health;
    public float speed;

    [Header("Drop")]
    public int numOfGold;
    public GameObject Gold;

    [Header("Movement")]
    public Transform target;
    public float nextWaypointDistance = 3f;
    public Path path;
    public int currentWaypoint = 0;

    FlyingEnemyBaseState currentState;
    public FlyingPatrolState PatrolState = new FlyingPatrolState();
    public FlyingAttackState AttackState = new FlyingAttackState();
    public FlyingHurtState HurtState = new FlyingHurtState();
    public FlyingDeadState DeadState = new FlyingDeadState();

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

    private void OnTriggerExit2D(Collider2D other)
    {
        currentState.OnTriggerExit2D(this, other);
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

    public void SwitchState(FlyingEnemyBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

    public void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    public void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void OnDrawGizmos()
    {
        
    }
}
