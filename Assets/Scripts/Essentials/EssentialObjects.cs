using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialObjects : MonoBehaviour
{

    public static EssentialObjects instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
