using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regale : MonoBehaviour
{
    public GameObject red;
    public GameObject purple;
    public GameObject blue;
    public GameObject green;
    public GameObject barkeeper;

    void Start()
    {
        if (SaveManager.instance.activeSave.purpleApe)
        {
            purple.SetActive(true);
            barkeeper.SetActive(true);
        }
        if (SaveManager.instance.activeSave.greenApe)
        {
            green.SetActive(true);
            barkeeper.SetActive(true);
        }
        if (SaveManager.instance.activeSave.blueApe)
        {
            blue.SetActive(true);
            barkeeper.SetActive(true);
        }
        if (SaveManager.instance.activeSave.redApe)
        {
            red.SetActive(true);
            barkeeper.SetActive(true);
        }
    }
}
