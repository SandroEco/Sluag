using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Letter : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" && Input.GetButton("Interact"))
        {
            other.GetComponent<Movement>().readLetter = 1;
            SaveManager.instance.activeSave.readLetter = 1;
            SaveManager.instance.Save();
        }
    }
}
