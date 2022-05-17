using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformStateManager : MonoBehaviour
{
    public GameObject currentPos;
    public bool canTransform;

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
}
