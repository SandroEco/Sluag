using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentPosition : MonoBehaviour
{
    public Transform player;

    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        transform.position = player.position;
    }
}
