using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHealth : MonoBehaviour
{
    public float totalHP;
    private float HP;

    void Awake()
    {
        HP = totalHP;
    }

    void Update()
    {
        if(HP <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Hit")
        {
            HP -= CombatRelated.damage;
            Debug.Log(HP);
        }
    }
}
