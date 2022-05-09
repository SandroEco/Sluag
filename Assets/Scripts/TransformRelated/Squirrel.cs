using UnityEngine;

public class Squirrel : TransformBaseState
{
    public InventoryScript IS;
    public TransformStateManager tSM;

    public override void EnterState(TransformStateManager transformStateManager)
    {
        IS = InventoryScript.FindObjectOfType<InventoryScript>();
        tSM = TransformStateManager.FindObjectOfType<TransformStateManager>();

        transformStateManager.sluagObject.SetActive(false);
        transformStateManager.circleObject.SetActive(true);
        transformStateManager.bearObject.SetActive(false);
        transformStateManager.frogObject.SetActive(false);

        transformStateManager.circleObject.transform.position = transformStateManager.currentPos.transform.position;
    }

    public override void UpdateState(TransformStateManager transformStateManager)
    {
        if (tSM.canTransform)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                transformStateManager.SwitchState(transformStateManager.sluag);
            }
            if (Input.GetKeyDown(KeyCode.A) && IS.squareShards >= 1)
            {
                transformStateManager.SwitchState(transformStateManager.bear);
            }
            if (Input.GetKeyDown(KeyCode.D) && IS.frogShards >= 1)
            {
                transformStateManager.SwitchState(transformStateManager.frog);
            }
        }
    }
}
