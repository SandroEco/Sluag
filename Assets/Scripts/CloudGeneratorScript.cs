using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudGeneratorScript : MonoBehaviour
{
    public GameObject[] clouds;

    public float spawnInterval;

    public GameObject endPoint;

    Vector2 startPos;

    void Start()
    {
        startPos = transform.position;
        Invoke("AttemptSpawn", spawnInterval);
    }

    void SpawnCloud(Vector2 startPos)
    {
        int randomIndex = UnityEngine.Random.Range(0, clouds.Length);
        GameObject cloud = Instantiate(clouds[UnityEngine.Random.Range(0, 4)]);

        float startY = UnityEngine.Random.Range(startPos.y - 3f, startPos.y + 3f);
        cloud.transform.position = new Vector2(startPos.x, startY);

        float speed = UnityEngine.Random.Range(0.5f, 1.5f);
        cloud.GetComponent<CloudScript>().StartFloating(speed, endPoint.transform.position.x);


    }

    void AttemptSpawn()
    {
        SpawnCloud(startPos);

        Invoke("AttemptSpawn", spawnInterval);

    }
}
