using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenApe : MonoBehaviour
{
    private AudioSource collectSource;
    public bool blue;
    public bool red;
    public bool purple;
    public bool green;
    void Start()
    {
        collectSource = GameObject.Find("SpecialCollectSource").GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (blue)
            {
                SaveManager.instance.activeSave.blueApe = true;
                SaveManager.instance.Save();
            }
            if (red)
            {
                SaveManager.instance.activeSave.redApe = true;
                SaveManager.instance.Save();
            }
            if (green)
            {
                SaveManager.instance.activeSave.greenApe = true;
                SaveManager.instance.Save();
            }
            if (purple)
            {
                SaveManager.instance.activeSave.purpleApe = true;
                SaveManager.instance.Save();
            }
            collectSource.Play();
            Destroy(gameObject);
        }
    }
}
