using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateManager : MonoBehaviour
{
    public static BossBaseState instance;

    public BossFightTrigger bossFightTrigger;
    public Animator anim;
    public GameObject shoot;
    public GameObject stone;
    public int count = 0;
    public int health = 3;
    public DialogManager dM;

    BossBaseState currentState;
    public BossFirstState FirstState = new BossFirstState();
    public BossSecondState SecondState = new BossSecondState();
    public BossThirdState ThirdState = new BossThirdState();

    void Start()
    {
        currentState = FirstState;

        currentState.EnterState(this);

        anim = GetComponent<Animator>();
        dM = FindObjectOfType<DialogManager>().GetComponent<DialogManager>();

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

    public void ShootProjectile()
    {
        Instantiate(shoot, new Vector2(transform.position.x -2, transform.position.y), Quaternion.identity);
    }

    public void ThrowRock()
    {
        Instantiate(stone, new Vector2(transform.position.x - 2, transform.position.y + 1), Quaternion.identity);
    }
}
