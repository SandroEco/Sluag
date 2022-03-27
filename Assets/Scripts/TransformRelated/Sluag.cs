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

        transformStateManager.sluagObject.transform.position = transformStateManager.currentPos.transform.position;
    }

    public override void UpdateState(TransformStateManager transformStateManager)
    {
        if (Input.GetKeyDown("r") && IS.circleShards >= 1)
        {
            //transformStateManager.currentPos.transform.position = transformStateManager.sluagObject.transform.position;
            transformStateManager.SwitchState(transformStateManager.circle);
        }
        if (Input.GetKeyDown("e") && IS.squareShards >= 1)
        {
            //transformStateManager.currentPos.transform.position = transformStateManager.sluagObject.transform.position;
            transformStateManager.SwitchState(transformStateManager.bear);
        }
    }
}
