using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHealth : MonoBehaviour
{
    public Rigidbody2D rb;

    public float totalHP;
    private float HP;
    public float knockback;
    public float knockbackTime;
    public bool isKnockbacked;

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Hit")
        {
            isKnockbacked = true;
            Vector2 difference = (transform.position - other.transform.position).normalized;
            Vector2 force = difference * knockback;
            rb.AddForce(force, ForceMode2D.Impulse);
            HP -= CombatRelated.damage;
            Debug.Log(HP);
            StartCoroutine(KnockbackCounter());
        }
    }

    private IEnumerator KnockbackCounter()
    {
        yield return new WaitForSeconds(knockbackTime);
        isKnockbacked = false;
    }
}
