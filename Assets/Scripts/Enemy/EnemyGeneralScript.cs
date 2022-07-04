using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneralScript : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rb;

    public float health;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

    }

    private void LateUpdate()
    {

    }
}

