using UnityEngine;

public class Circle : TransformBaseState
{
    public InventoryScript IS;

    public override void EnterState(TransformStateManager transformStateManager)
    {
        IS = InventoryScript.FindObjectOfType<InventoryScript>();

        transformStateManager.sluagObject.SetActive(false);
        transformStateManager.circleObject.SetActive(true);
        transformStateManager.squareObject.SetActive(false);

        transformStateManager.circleObject.transform.position = transformStateManager.currentPos.transform.position;
    }

    public override void UpdateState(TransformStateManager transformStateManager)
    {
        if (Input.GetKeyDown("v"))
        {
            transformStateManager.currentPos.transform.position = transformStateManager.circleObject.transform.position;
            transformStateManager.SwitchState(transformStateManager.sluag);
        }
        if (Input.GetKeyDown("c") && IS.squareShards >= 1)
        {
            transformStateManager.currentPos.transform.position = transformStateManager.circleObject.transform.position;
            transformStateManager.SwitchState(transformStateManager.square);
        }
    }
}
