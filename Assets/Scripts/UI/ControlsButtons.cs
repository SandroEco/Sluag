using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsButtons : MonoBehaviour
{
    public GameObject buttons;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            buttons.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

}
