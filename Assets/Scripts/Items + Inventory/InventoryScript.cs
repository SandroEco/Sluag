using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{
    public static InventoryScript instance;

    [Header("Soulshards")]
    public int circleShards = 0;
    public int squareShards = 0;

    [Header("Gold")]
    public int gold = 0;

    private void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
}
