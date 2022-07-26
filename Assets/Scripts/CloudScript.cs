using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudScript : MonoBehaviour
{
    float _speed = 2;
    float _endPosX;

    public void StartFloating(float speed, float endPosX)
    {
        _speed = speed;
        _endPosX = endPosX;
    }

    void Update()
    {
        transform.Translate(-Vector2.right * (Time.deltaTime * _speed));

        if(transform.position.x < _endPosX)
        {
            Destroy(gameObject);
        }
    }
}
