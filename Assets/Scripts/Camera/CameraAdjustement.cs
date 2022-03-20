using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraAdjustement : MonoBehaviour
{
    CinemachineVirtualCamera cva;
    public Transform player;

    private void Start()
    {
        cva = GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        cva.LookAt = player;
        cva.Follow = player;
    }
}
