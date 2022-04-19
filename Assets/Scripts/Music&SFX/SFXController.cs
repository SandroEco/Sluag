using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : MonoBehaviour
{
    //public AudioSource footstep;

    private void Footstep()
    {
        //footstep.Play();
        AudioManager.instance.FootstepSFX();
    }
}
