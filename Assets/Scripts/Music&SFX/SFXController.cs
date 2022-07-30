using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : MonoBehaviour
{
    public AudioSource jumpSound;
    public AudioSource punchSound;
    public AudioSource punchWhooshSound;
    public AudioSource landSound;
    public AudioSource demonSnarl;
    public AudioSource torchesOnSound;
    public AudioSource openChestSound;
    public AudioSource gotHitSound;
    public AudioSource lostSource;
    public AudioSource openPfostenSource;

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

    public void PunchWhooshSound()
    {
        punchWhooshSound.Play();
    }

    public void LandSound()
    {
        landSound.Play();
    }

    public void Tier1DemonSnarl()
    {
        demonSnarl.Play();
    }

    public void TorchesOnSound()
    {
        torchesOnSound.Play();
    }

    public void OpenChestSound()
    {
        openChestSound.Play();
    }

    public void GotHitSound()
    {
        gotHitSound.Play();
    }

    public void LostSound()
    {
        lostSource.Play();
    }

    public void OpenPfostenSound()
    {
        openPfostenSource.Play();
    }
}
