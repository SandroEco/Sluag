using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{
    public static InventoryScript instance;

    [Header("Soulshards")]
    public int circleShards;
    public int squareShards;

    [Header("Gold")]
    public int gold;

    [Header("Keys")]
    public int key;

    private void Start()
    {
        if(instance == null)
        {
            instance = this;
        }

        if (SaveManager.instance.hasLoaded)
        {
            circleShards = SaveManager.instance.activeSave.circleShards;
            squareShards = SaveManager.instance.activeSave.squareShards;
            gold = SaveManager.instance.activeSave.gold;
            key = SaveManager.instance.activeSave.key;
        }
        else
        {
            SaveManager.instance.activeSave.circleShards = circleShards;
            SaveManager.instance.activeSave.squareShards = squareShards;
            SaveManager.instance.activeSave.gold = gold;
            SaveManager.instance.activeSave.key = key;
        }
    }
}
