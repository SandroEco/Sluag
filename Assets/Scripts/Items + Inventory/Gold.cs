using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
    public InventoryScript IS;
    private Rigidbody2D rb;
    private CircleCollider2D cc;
    public AudioSource coinCollectSource;

    public int goldValue;
    private void Start()
    {
        IS = FindObjectOfType<InventoryScript>();
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CircleCollider2D>();
        coinCollectSource = GameObject.Find("CoinCollectSource").GetComponent<AudioSource>();

        float directionX = Random.Range(-2f, 2f);
        float directionY = Random.Range(1f, 3f);

        float force = Random.Range(2, 3);
        rb.AddForce(new Vector2 (directionX, directionY) * force, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == ("Player"))
        {
            IS.gold = IS.gold + goldValue;
            coinCollectSource.Play();
            SaveManager.instance.activeSave.gold = IS.gold;
            IS.text.text = IS.gold.ToString();
            Destroy(gameObject);
        }
    }

    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            IS.gold = IS.gold + goldValue;
            SaveManager.instance.activeSave.gold = IS.gold;
            IS.text.text = IS.gold.ToString();
            Destroy(cc);
        }
    }
    */
}
