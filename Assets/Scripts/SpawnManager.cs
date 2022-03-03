using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;

    [SerializeField] private float spawnRange;

    void Start()
    {
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        Instantiate(enemyPrefab, GetSpawnPosition(), enemyPrefab.transform.rotation);
    }

    private Vector3 GetSpawnPosition()
    {
        float posX = Random.Range(-spawnRange, spawnRange);
        float posZ = Random.Range(-spawnRange, spawnRange);

        return new Vector3(posX, 0, posZ);
    }
}
