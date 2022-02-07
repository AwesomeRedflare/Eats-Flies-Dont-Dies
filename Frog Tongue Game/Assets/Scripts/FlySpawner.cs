using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlySpawner : MonoBehaviour
{
    public GameObject fly;

    public float minX;
    public float minY;
    public float maxX;
    public float maxY;

    public float minTime;
    public float maxTime;

    private void Start()
    {
        StartCoroutine("SpawnFlies");
    }

    IEnumerator SpawnFlies()
    {
        Vector2 randomPos = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));

        yield return new WaitForSeconds(Random.Range(minTime, maxTime));

        Instantiate(fly, randomPos, fly.transform.rotation);

        StartCoroutine("SpawnFlies");
    }
}
