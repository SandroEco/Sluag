using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acceleration : MonoBehaviour
{
    private Animator anim;
    public bool setMaxSpeedReached;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (setMaxSpeedReached)
        {
            anim.SetBool("MaxSpeedReached", true);
        }
        if (!setMaxSpeedReached)
        {
            anim.SetBool("MaxSpeedReached", false);

        }
    }
}
