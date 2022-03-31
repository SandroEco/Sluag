using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraAdjustement : MonoBehaviour
{
    CinemachineVirtualCamera cva;
    public Transform currentPos;


    private void Start()
    {
        cva = GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        currentPos = GameObject.FindGameObjectWithTag("CurrentPos").transform;
        cva.LookAt = currentPos;
        cva.Follow = currentPos;
    }
}
