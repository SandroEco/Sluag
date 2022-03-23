using UnityEngine;

public class Circle : TransformBaseState
{
    public InventoryScript IS;

    public override void EnterState(TransformStateManager transformStateManager)
    {
        IS = InventoryScript.FindObjectOfType<InventoryScript>();

        transformStateManager.sluagObject.SetActive(false);
        transformStateManager.circleObject.SetActive(true);
        transformStateManager.bearObject.SetActive(false);

        transformStateManager.circleObject.transform.position = transformStateManager.currentPos.transform.position;
    }

    public override void UpdateState(TransformStateManager transformStateManager)
    {
        if (Input.GetKeyDown("r"))
        {
            transformStateManager.currentPos.transform.position = transformStateManager.circleObject.transform.position;
            transformStateManager.SwitchState(transformStateManager.sluag);
        }
        if (Input.GetKeyDown("e") && IS.squareShards >= 1)
        {
            transformStateManager.currentPos.transform.position = transformStateManager.circleObject.transform.position;
            transformStateManager.SwitchState(transformStateManager.bear);
        }
    }
}
