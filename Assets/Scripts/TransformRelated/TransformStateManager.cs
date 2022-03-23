using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformStateManager : MonoBehaviour
{
    public GameObject currentPos;

    [Header("Souls")]
    public GameObject sluagObject;
    public GameObject circleObject;
    public GameObject bearObject;

    TransformBaseState currentState;
    public Sluag sluag = new Sluag();
    public Circle circle = new Circle();
    public Bear bear = new Bear();

    void Start()
    {
        currentState = sluag;

        currentState.EnterState(this);
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
}
