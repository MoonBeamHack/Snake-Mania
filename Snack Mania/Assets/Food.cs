using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public Bounds SpawnArea;
    private void Start()
    {
        RePosition();
    }
    public void RePosition()
    {
        float randomX = (int)Random.Range(SpawnArea.min.x, SpawnArea.max.x);
        float randomY = (int)Random.Range(SpawnArea.min.y, SpawnArea.max.y);

        transform.position = new Vector3(randomX, randomY);
    }
}
