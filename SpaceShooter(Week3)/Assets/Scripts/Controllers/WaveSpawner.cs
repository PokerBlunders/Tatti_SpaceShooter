using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRadius = 10f;
    public Transform playerTransform;

    private int waveNumber = 1;
    List<GameObject> activeEnemies = new List<GameObject>();

    void Start()
    {
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        yield return new WaitForSeconds(2f);

        while (true)
        {
            for (int i = 0; i < waveNumber; i++)
            {
                SpawnEnemy();
            }

            while (activeEnemies.Count > 0)
            {
                yield return null;
            }

            waveNumber++;
            yield return new WaitForSeconds(2f);
        }
    }

    void SpawnEnemy()
    {
        Vector3 spawnPosition = RandomSpawnPosition();
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        enemy.GetComponent<EnemyAttack>().playerTransform = playerTransform;
        activeEnemies.Add(enemy);
    }

    Vector3 RandomSpawnPosition()
    {
        Vector3 randomPos = Vector3.zero;
        bool Position = false;

        while (!Position)
        {
            Vector3 randomOffset = new Vector3(Random.Range(-spawnRadius, spawnRadius), Random.Range(-spawnRadius, spawnRadius), 0f);
            randomPos = transform.position + randomOffset;

            if (Vector3.Distance(randomPos, playerTransform.position) > 2f)
            {
                Position = true;
            }
        }

        return randomPos;
    }

    public List<GameObject> GetActiveEnemies()
    {
        return activeEnemies;
    }

    public void EnemyDestroyed(GameObject enemy)
    {
        activeEnemies.Remove(enemy);
    }
}