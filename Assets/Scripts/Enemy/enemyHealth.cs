using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody2D rb;
    public BearEnemy bearEnemy;
    private ItemDrop iD;

    public float totalHP;
    private float HP;
    public float knockback;
    public float knockbackTime;
    public bool isKnockbacked;

    void Awake()
    {
        HP = totalHP;
    }

    private void Start()
    {
        bearEnemy = GetComponent<BearEnemy>();
    }

    void Update()
    {
        if(HP <= 0)
        {
            Dead();
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
            StartCoroutine(KnockbackCounter());

            HP -= CombatRelated.damage;
            Debug.Log(HP);
        }
    }

    private IEnumerator KnockbackCounter()
    {
        yield return new WaitForSeconds(knockbackTime);
        isKnockbacked = false;
    }

    private void Dead()
    {
        //play death anim
        iD = GetComponent<ItemDrop>();
        iD.Drop();
    }
}
