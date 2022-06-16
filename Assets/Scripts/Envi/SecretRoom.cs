using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretRoom : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            anim.SetTrigger("Revealed");
            StartCoroutine(EndOfReveal());
        }
    }

    private IEnumerator EndOfReveal()
    {
        yield return new WaitForSeconds(3);
        this.gameObject.SetActive(false);
    }
}
