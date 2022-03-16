using UnityEngine;

public class Square : TransformBaseState
{
    public InventoryScript IS;

    public override void EnterState(TransformStateManager transformStateManager)
    {
        IS = InventoryScript.FindObjectOfType<InventoryScript>();

        transformStateManager.sluagObject.SetActive(false);
        transformStateManager.circleObject.SetActive(false);
        transformStateManager.squareObject.SetActive(true);

        transformStateManager.squareObject.transform.position = transformStateManager.currentPos.transform.position;
    }

    public override void UpdateState(TransformStateManager transformStateManager)
    {
        if (Input.GetKeyDown("v") && IS.circleShards >= 1)
        {
            transformStateManager.currentPos.transform.position = transformStateManager.squareObject.transform.position;
            transformStateManager.SwitchState(transformStateManager.circle);
        }
        if (Input.GetKeyDown("c"))
        {
            transformStateManager.currentPos.transform.position = transformStateManager.squareObject.transform.position;
            transformStateManager.SwitchState(transformStateManager.sluag);
        }
    }
}
