using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformStateManager : MonoBehaviour
{
    public GameObject currentPos;
    public bool canTransform;
    public Animator anim;
    public Rigidbody2D rb;

    [Header("Souls")]
    public GameObject sluagObject;
    public GameObject circleObject;
    public GameObject bearObject;
    public GameObject frogObject;

    TransformBaseState currentState;
    public Sluag sluag = new Sluag();
    public Squirrel squirrel = new Squirrel();
    public Bear bear = new Bear();
    public Frog frog = new Frog();



    void Start()
    {
        currentState = sluag;

        currentState.EnterState(this);

        canTransform = true;
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(TransformBaseState transformBaseState)
    {
        currentState = transformBaseState;
        transformBaseState.EnterState(this);
    }

    public IEnumerator TransformationCooldown()
    {
        canTransform = false;
        yield return new WaitForSeconds(0.5f);
        canTransform = true;
    }

    public void TransformTransition()
    {
        StartCoroutine(TransformationTransition());
    }

    public void TransformTransitionBack()
    {
        StartCoroutine(TransformTransitionBackwards());
    }

    public void TransformTransitionBackBear()
    {
        StartCoroutine(TransformTransitionBackwardsBear());
    }

    private IEnumerator TransformationTransition()
    {
        anim.SetTrigger("Transform");
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(0.5f);
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private IEnumerator TransformTransitionBackwards()
    {
        anim.SetTrigger("TransformBack");
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(0.5f);
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        SwitchState(squirrel);
        StartCoroutine(TransformationCooldown());
    }

    private IEnumerator TransformTransitionBackwardsBear()
    {
        anim.SetTrigger("TransformBack");
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(0.5f);
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        SwitchState(bear);
        StartCoroutine(TransformationCooldown());
    }
}
