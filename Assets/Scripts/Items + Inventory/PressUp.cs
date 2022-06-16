using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressUp : MonoBehaviour
{
    public GameObject pressUp;
    private InventoryScript inventory;

    [Header("Optional")]
    public bool needsKey;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && !needsKey)
        {
            pressUp.SetActive(true);
        }
        if(other.tag == "Player" && needsKey)
        {
            if(InventoryScript.instance.key >= 1)
            {
                pressUp.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            pressUp.SetActive(false);
        }
    }

}
