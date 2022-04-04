using UnityEngine;

public class Bear : TransformBaseState
{
    public InventoryScript IS;

    public override void EnterState(TransformStateManager transformStateManager)
    {
        IS = InventoryScript.FindObjectOfType<InventoryScript>();

        transformStateManager.sluagObject.SetActive(false);
        transformStateManager.circleObject.SetActive(false);
        transformStateManager.bearObject.SetActive(true);

        transformStateManager.bearObject.transform.position = transformStateManager.currentPos.transform.position;
    }

    public override void UpdateState(TransformStateManager transformStateManager)
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && IS.circleShards >= 1)
        {
            //transformStateManager.currentPos.transform.position = transformStateManager.bearObject.transform.position;
            transformStateManager.SwitchState(transformStateManager.circle);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //transformStateManager.currentPos.transform.position = transformStateManager.bearObject.transform.position;
            transformStateManager.SwitchState(transformStateManager.sluag);
        }
    }
}
