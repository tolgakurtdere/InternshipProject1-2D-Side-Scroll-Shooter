using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy1 : MonoBehaviour
{
    public GameObject enemyPrefab;
    float spawnTime = 0, spawnTimeControl = 2f;
    Vector3 tempPos;

    private void Start()
    {
        tempPos = transform.position;
    }
    void Update()
    {
        spawnTime += Time.deltaTime;
        if (spawnTime > spawnTimeControl)
        {
            spawnTime = 0;
            Instantiate(enemyPrefab, tempPos, Quaternion.identity);
            tempPos = tempPos + new Vector3(3,0,0);
        }
    }

}
