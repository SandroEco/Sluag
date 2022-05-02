using UnityEngine;

public class Frog : TransformBaseState
{
    public InventoryScript IS;

    public override void EnterState(TransformStateManager transformStateManager)
    {
        IS = InventoryScript.FindObjectOfType<InventoryScript>();

        transformStateManager.sluagObject.SetActive(false);
        transformStateManager.circleObject.SetActive(false);
        transformStateManager.bearObject.SetActive(false);
        transformStateManager.frogObject.SetActive(true);

        transformStateManager.frogObject.transform.position = transformStateManager.currentPos.transform.position;
    }

    public override void UpdateState(TransformStateManager transformStateManager)
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && IS.circleShards >= 1)
        {
            transformStateManager.SwitchState(transformStateManager.squirrel);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && IS.squareShards >= 1)
        {
            transformStateManager.SwitchState(transformStateManager.bear);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transformStateManager.SwitchState(transformStateManager.sluag);
        }
    }
}