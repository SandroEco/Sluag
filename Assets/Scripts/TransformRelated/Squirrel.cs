using UnityEngine;

public class Squirrel : TransformBaseState
{
    public InventoryScript IS;

    public override void EnterState(TransformStateManager transformStateManager)
    {
        IS = InventoryScript.FindObjectOfType<InventoryScript>();

        transformStateManager.sluagObject.SetActive(false);
        transformStateManager.circleObject.SetActive(true);
        transformStateManager.bearObject.SetActive(false);
        transformStateManager.frogObject.SetActive(false);

        transformStateManager.circleObject.transform.position = transformStateManager.currentPos.transform.position;
    }

    public override void UpdateState(TransformStateManager transformStateManager)
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transformStateManager.SwitchState(transformStateManager.sluag);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && IS.squareShards >= 1)
        {
            transformStateManager.SwitchState(transformStateManager.bear);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && IS.frogShards >= 1)
        {
            transformStateManager.SwitchState(transformStateManager.frog);
        }
    }
}
