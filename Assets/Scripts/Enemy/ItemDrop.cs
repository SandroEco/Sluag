using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public GameObject drop;
    public GameObject goldDrop;
    public int numOfGold;

    public void Drop()
    {
        for (int i = 0; i < numOfGold; i++)
        {
            goldDrop = Instantiate(goldDrop, transform.position, Quaternion.identity);
        }

        if (!drop)
        {
            return;
        }
        else
        {
            Instantiate(drop, transform.position, Quaternion.identity);
        }
    }
}
