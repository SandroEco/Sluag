using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    public Animator anim;
    public GameObject fade;
    public static Fade instance;

    private void Awake()
    {
        instance = this;
        anim.SetTrigger("FadeOut");
        StartCoroutine(Disable());
    }

    public void FadeIn()
    {
        fade.gameObject.SetActive(true);
        anim.SetTrigger("FadeIn");
    }

    private IEnumerator Disable()
    {
        yield return new WaitForSeconds(1.2f);
        fade.gameObject.SetActive(false); 
    }
}
