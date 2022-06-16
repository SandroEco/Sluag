using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryScript : MonoBehaviour
{
    public static InventoryScript instance;

    [Header("Soulshards")]
    public int circleShards;
    public int squareShards;
    public int frogShards;

    [Header("Gold")]
    public int gold;
    public TextMeshProUGUI text;

    [Header("Keys")]
    public int key;
    public int waterBottle;

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
            frogShards = SaveManager.instance.activeSave.frogShards;
            gold = SaveManager.instance.activeSave.gold;
            key = SaveManager.instance.activeSave.key;
            waterBottle = SaveManager.instance.activeSave.waterBottle;
            text.text = gold.ToString();
        }
        else
        {
            SaveManager.instance.activeSave.circleShards = circleShards;
            SaveManager.instance.activeSave.squareShards = squareShards;
            SaveManager.instance.activeSave.frogShards = frogShards;
            SaveManager.instance.activeSave.gold = gold;
            SaveManager.instance.activeSave.key = key;
            SaveManager.instance.activeSave.waterBottle = waterBottle;
        }
    }
}
