using UnityEngine;

public class Bear : TransformBaseState
{
    public InventoryScript IS;
    public TransformStateManager tSM;

    public override void EnterState(TransformStateManager transformStateManager)
    {
        IS = InventoryScript.FindObjectOfType<InventoryScript>();
        tSM = TransformStateManager.FindObjectOfType<TransformStateManager>();

        transformStateManager.sluagObject.SetActive(false);
        transformStateManager.circleObject.SetActive(false);
        transformStateManager.bearObject.SetActive(true);
        transformStateManager.frogObject.SetActive(false);

        transformStateManager.bearObject.transform.position = transformStateManager.currentPos.transform.position;
    }

    public override void UpdateState(TransformStateManager transformStateManager)
    {
        if (tSM.canTransform)
        {
            if (Input.GetKeyDown(KeyCode.W) && IS.circleShards >= 1)
            {
                transformStateManager.SwitchState(transformStateManager.squirrel);
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                transformStateManager.SwitchState(transformStateManager.sluag);
            }
            if (Input.GetKeyDown(KeyCode.D) && IS.frogShards >= 1)
            {
                transformStateManager.SwitchState(transformStateManager.frog);
            }
        }
    }
}
