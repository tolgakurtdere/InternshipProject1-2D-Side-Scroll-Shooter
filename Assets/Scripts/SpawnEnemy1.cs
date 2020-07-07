using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy1 : MonoBehaviour
{
    public GameObject enemyPrefab;
    float spawnTime = 0, spawnTimeControl = 4f;
    void Start()
    {
        Instantiate(enemyPrefab, transform.position, Quaternion.identity); //create enemy at the beginning
    }

    void Update()
    {
        LevelCheck();
        spawnTime += Time.deltaTime;
        if (spawnTime > spawnTimeControl)
        {
            spawnTime = 0;
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        }
    }

    private void LevelCheck() //fix spawn speed according to level
    {
        if (GameControl.level == 2) spawnTimeControl = 3f;
        else if (GameControl.level == 3) spawnTimeControl = 1.5f;
        else if (GameControl.level == 4) spawnTimeControl = 0.75f;
        else if (GameControl.level >= 5) spawnTimeControl = 0.5f;
    }
}
