using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneThrow : MonoBehaviour
{
    Rigidbody2D rb;
    public float force;
    public Vector2 direction;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(direction * force, ForceMode2D.Impulse);
        StartCoroutine(DestroyStone());
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Hit" || other.tag == "Hit 2")
        {
            rb.AddForce(Vector2.right * (force * 4), ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Demon")
        {
            Destroy(gameObject);
        }

    }

    private IEnumerator DestroyStone()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
