using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject powerupPrefab;

    [SerializeField] private float spawnRange;

    private int enemyCount;
    private int waveNumber;

    private static SpawnManager instance;

    void Start()
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;

        enemyCount = 0;
        waveNumber = 1;

        CreateWave(waveNumber);
    }

    private void CreateWave(int amount)
    {
        for(int i = 0; i < waveNumber; i++)
        {
            Instantiate(enemyPrefab, GetSpawnPosition(), enemyPrefab.transform.rotation);
            Instantiate(powerupPrefab, GetSpawnPosition(), powerupPrefab.transform.rotation);
            enemyCount++;
        }
    }

    private Vector3 GetSpawnPosition()
    {
        float posX = Random.Range(-spawnRange, spawnRange);
        float posZ = Random.Range(-spawnRange, spawnRange);

        return new Vector3(posX, 0, posZ);
    }

    public void EnemyDeath()
    {
        enemyCount--;

        if(enemyCount <= 0)
        {
            waveNumber++;
            CreateWave(waveNumber);
        }
    }

    public static SpawnManager GetInstance()
    {
        return instance;
    }
}
