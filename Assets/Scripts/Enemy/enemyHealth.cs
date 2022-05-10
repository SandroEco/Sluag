using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody2D rb;
    public BearEnemy bearEnemy;
    private ItemDrop iD;
    private Animator anim;

    public float totalHP;
    public float HP;
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
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(HP <= 0)
        {
            StartCoroutine(Dead());
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
            //Debug.Log(HP);
        }
    }

    private IEnumerator KnockbackCounter()
    {
        yield return new WaitForSeconds(knockbackTime);
        isKnockbacked = false;
    }

    private IEnumerator Dead()
    {
        rb.velocity = new Vector2(0, 0);
        anim.SetTrigger("Dead");
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
        iD = GetComponent<ItemDrop>();
        iD.Drop();
    }
}
