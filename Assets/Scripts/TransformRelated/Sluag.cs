using UnityEngine;

public class Sluag : TransformBaseState
{
    public InventoryScript IS;

    public override void EnterState(TransformStateManager transformStateManager)
    {
        IS = InventoryScript.FindObjectOfType<InventoryScript>();

        transformStateManager.sluagObject.SetActive(true);
        transformStateManager.circleObject.SetActive(false);
        transformStateManager.bearObject.SetActive(false);
        transformStateManager.frogObject.SetActive(false);

        transformStateManager.sluagObject.transform.position = transformStateManager.currentPos.transform.position;
    }

    public override void UpdateState(TransformStateManager transformStateManager)
    {
        if (Input.GetKeyDown(KeyCode.W) && IS.circleShards >= 1)
        {
            transformStateManager.SwitchState(transformStateManager.squirrel);
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
