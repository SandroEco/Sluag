using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : MonoBehaviour
{
    public AudioSource jumpSound;
    public AudioSource punchSound;

    private void Footstep()
    {
        //footstep.Play();
        AudioManager.instance.FootstepSFX();
    }

    public void JumpSound()
    {
        jumpSound.Play();
    }

    public void PunchSound()
    {
        punchSound.Play();
    }

}
