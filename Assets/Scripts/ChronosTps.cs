using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChronosTps : MonoBehaviour
{
    public GameObject deadBear;
    public GameObject chronos;

    void Start()
    {
        
    }

    void Update()
    {
        if(deadBear == null)
        {
            chronos.SetActive(true);
        }
        else
        {
            return;
        }
    }
}
