using UnityEngine;

public class Frog : TransformBaseState
{
    public InventoryScript IS;
    public TransformStateManager tSM;

    public override void EnterState(TransformStateManager transformStateManager)
    {
        IS = InventoryScript.FindObjectOfType<InventoryScript>();
        tSM = TransformStateManager.FindObjectOfType<TransformStateManager>();

        transformStateManager.sluagObject.SetActive(false);
        transformStateManager.circleObject.SetActive(false);
        transformStateManager.bearObject.SetActive(false);
        transformStateManager.frogObject.SetActive(true);

        transformStateManager.frogObject.transform.position = transformStateManager.currentPos.transform.position;
    }

    public override void UpdateState(TransformStateManager transformStateManager)
    {
        if (tSM.canTransform)
        {
            if (Input.GetKeyDown(KeyCode.W) && IS.circleShards >= 1)
            {
                transformStateManager.SwitchState(transformStateManager.squirrel);
            }
            if (Input.GetKeyDown(KeyCode.A) && IS.squareShards >= 1)
            {
                transformStateManager.SwitchState(transformStateManager.bear);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                transformStateManager.SwitchState(transformStateManager.sluag);
            }
        }  
    }
}
