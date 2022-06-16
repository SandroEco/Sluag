using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CantTransform : MonoBehaviour
{

    public TransformStateManager tSM;
    public PolygonCollider2D pC;

    void Start()
    {
        tSM = TransformStateManager.FindObjectOfType<TransformStateManager>();
        pC = GetComponent<PolygonCollider2D>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "CantTransform")
        {
            tSM.canTransform = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "CantTransform")
        {
            tSM.canTransform = true;
        }
    }
}
