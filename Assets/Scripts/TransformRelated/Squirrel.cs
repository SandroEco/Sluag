using UnityEngine;

public class Squirrel : TransformBaseState
{
    public InventoryScript IS;
    public TransformStateManager tSM;

    public override void EnterState(TransformStateManager transformStateManager)
    {
        IS = InventoryScript.FindObjectOfType<InventoryScript>();
        tSM = TransformStateManager.FindObjectOfType<TransformStateManager>();

        transformStateManager.sluagObject.SetActive(false);
        transformStateManager.circleObject.SetActive(true);
        transformStateManager.bearObject.SetActive(false);
        transformStateManager.frogObject.SetActive(false);

        transformStateManager.circleObject.transform.position = transformStateManager.currentPos.transform.position;
    }

    public override void UpdateState(TransformStateManager transformStateManager)
    {
        if (tSM.canTransform)
        {
            if (Input.GetButtonDown("Transformation2"))
            {
                transformStateManager.SwitchState(transformStateManager.sluag);
                tSM.StartCoroutine(tSM.TransformationCooldown());

            }
            if (Input.GetButtonDown("Transformation1") && IS.squareShards >= 1)
            {
                transformStateManager.SwitchState(transformStateManager.bear);
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
