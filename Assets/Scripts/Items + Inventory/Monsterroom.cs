using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monsterroom : MonoBehaviour
{
    public List <GameObject> enemies = new List<GameObject>();
    BoxCollider2D bc;
    Animator anim;

    void Start()
    {
        bc = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        for(var i =enemies.Count - 1; i > -1; i--)
        {
            if(enemies[i] == null)
            {
                enemies.RemoveAt(i);
            }
        }

        if(enemies.Count == 0)
        {
            anim.SetTrigger("Open");
        }
    }
}
