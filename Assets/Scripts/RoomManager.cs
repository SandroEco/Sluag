using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class RoomManager : MonoBehaviour
{
    public Transform target;

    public GameObject virtualCam;
    CinemachineVirtualCamera vcam;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            virtualCam.SetActive(true);
        }

        vcam = GameObject.FindGameObjectWithTag("Cameras").GetComponent<CinemachineVirtualCamera>();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            virtualCam.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        vcam.LookAt = target.transform;
        vcam.Follow = target.transform;
    }
}
