using UnityEngine;

public abstract class TransformBaseState 
{
    public abstract void EnterState(TransformStateManager transformStateManager);

    public abstract void UpdateState(TransformStateManager transformStateManager);
}
