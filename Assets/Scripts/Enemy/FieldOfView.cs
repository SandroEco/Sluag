using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public Vector2 radius;
    public LayerMask player;
    private float angle;

    public GameObject enemy;

    private void Update()
    {
        if (enemy)
        {
            FieldOfViewCheck();
        }
    }

    void FieldOfViewCheck()
    {
        if(Physics2D.OverlapBox(transform.position, radius, angle, player))
        {
            enemy.SetActive(true);
        }
        else
        {
            enemy.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, radius);
    }
}
