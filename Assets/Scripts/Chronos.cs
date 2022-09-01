using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chronos : MonoBehaviour
{
    private Animator anim;
    private DialogTrigger dt;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        dt = GetComponent<DialogTrigger>();
    }

    void Start()
    {
        anim.SetTrigger("tp");
        StartCoroutine(DialogStart());
    }

    private IEnumerator DialogStart()
    {
        yield return new WaitForSeconds(1.1f);
        dt.StartDialog();
    }
}
