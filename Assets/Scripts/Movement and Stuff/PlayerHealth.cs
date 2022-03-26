using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody2D rb;
    public HealthAll healtAllScript;

    [Header("Knockback")]
    public float knockback;
    public float knockbackTime;
    public bool isKnockbacked;

    void Start()
    {
        isKnockbacked = false;
        healtAllScript = GameObject.FindObjectOfType<HealthAll>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "BearAntlers")
        {
            rb = GetComponent<Rigidbody2D>();
            isKnockbacked = true;
            Vector2 difference = (transform.position - other.transform.position);
            difference.y = 1f;
            Vector2 force = difference * knockback;
            rb.AddForce(force, ForceMode2D.Impulse);
            healtAllScript.TakeDamage(10);
            StartCoroutine(KnockbackCounter());
        }
    }

    private IEnumerator KnockbackCounter()
    {
        yield return new WaitForSeconds(knockbackTime);
        isKnockbacked = false;
    }
}
