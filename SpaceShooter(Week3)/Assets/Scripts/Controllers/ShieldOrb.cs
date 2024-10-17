using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldOrb : MonoBehaviour
{
    public GameObject shieldPrefab;
    public float shieldRadius = 2.75f;
    public float shieldDuration = 5f;
    public float collectionRadius = 0.5f;
    public Transform playerTransform;
    GameObject shieldInstance;
    bool isShieldActive = false;
    bool isCollected = false;
    public GameObject orbShell;

    private WaveSpawner waveSpawner;

    void Start()
    {
        waveSpawner = FindObjectOfType<WaveSpawner>();
    }

    void Update()
    {
        if (!isCollected)
        {
            CheckForCollection();
        }

        if (isShieldActive)
        {
            CheckForObjectsToDestroy();
        }
    }

    void CheckForCollection()
    {
        if (Vector3.Distance(transform.position, playerTransform.position) < collectionRadius)
        {
            CollectShieldOrb();
        }
    }

    public void CollectShieldOrb()
    {
        if (isShieldActive) return;

        shieldInstance = Instantiate(shieldPrefab, playerTransform.position, Quaternion.identity);
        shieldInstance.transform.SetParent(playerTransform);
        isShieldActive = true;
        isCollected = true;

        orbShell.SetActive(false);

        StartCoroutine(DeactivateShield());
    }

    IEnumerator DeactivateShield()
    {
        yield return new WaitForSeconds(shieldDuration);

        Destroy(shieldInstance);
        isShieldActive = false;

        Destroy(gameObject);
    }

    void CheckForObjectsToDestroy()
    {
        if (waveSpawner == null) return;

        List<GameObject> activeEnemies = waveSpawner.GetActiveEnemies();
        for (int i = activeEnemies.Count - 1; i >= 0; i--)
        {
            GameObject enemy = activeEnemies[i];
            if (Vector3.Distance(playerTransform.position, enemy.transform.position) < shieldRadius)
            {
                Destroy(enemy);
                waveSpawner.EnemyDestroyed(enemy);
            }
        }
    }
}
