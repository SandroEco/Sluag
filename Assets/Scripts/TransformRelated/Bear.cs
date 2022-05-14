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
            if (Input.GetButtonDown("Transformation2") && IS.circleShards >= 1)
            {
                transformStateManager.SwitchState(transformStateManager.squirrel);
                tSM.StartCoroutine(tSM.TransformationCooldown());
            }
            if (Input.GetButtonDown("Transformation1"))
            {
                transformStateManager.SwitchState(transformStateManager.sluag);
                tSM.StartCoroutine(tSM.TransformationCooldown());
            }
            if (Input.GetButtonDown("Transformation3") && IS.frogShards >= 1 || Input.GetAxisRaw("Transformation3Controller") == 1 && IS.frogShards >= 1)
            {
                transformStateManager.SwitchState(transformStateManager.frog);
                tSM.StartCoroutine(tSM.TransformationCooldown());

            }
        }
    }
}
