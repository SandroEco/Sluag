using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            StartCoroutine(Freeze());
        }
    }

    private IEnumerator Freeze()
    {
        Time.timeScale = 0.0f;
        yield return new WaitForSecondsRealtime(0.6f);
        Time.timeScale = 1f;
    }
}
