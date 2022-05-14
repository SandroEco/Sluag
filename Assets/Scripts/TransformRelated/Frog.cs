using UnityEngine;

public class Frog : TransformBaseState
{
    public InventoryScript IS;
    public TransformStateManager tSM;

    public override void EnterState(TransformStateManager transformStateManager)
    {
        IS = InventoryScript.FindObjectOfType<InventoryScript>();
        tSM = TransformStateManager.FindObjectOfType<TransformStateManager>();

        transformStateManager.sluagObject.SetActive(false);
        transformStateManager.circleObject.SetActive(false);
        transformStateManager.bearObject.SetActive(false);
        transformStateManager.frogObject.SetActive(true);

        transformStateManager.frogObject.transform.position = transformStateManager.currentPos.transform.position;
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
            if (Input.GetButtonDown("Transformation1") && IS.squareShards >= 1)
            {
                transformStateManager.SwitchState(transformStateManager.bear);
                tSM.StartCoroutine(tSM.TransformationCooldown());

            }
            if (Input.GetButtonDown("Transformation3") || Input.GetAxisRaw("Transformation3Controller") == 1)
            {
                transformStateManager.SwitchState(transformStateManager.sluag);
                tSM.StartCoroutine(tSM.TransformationCooldown());

            }
        }  
    }
}
