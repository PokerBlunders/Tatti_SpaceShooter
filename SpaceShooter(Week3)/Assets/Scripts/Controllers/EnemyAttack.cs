using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Transform playerTransform;
    public float moveSpeed = 1f;

    void Update()
    {
        MoveTowardsPlayer();
    }

    void MoveTowardsPlayer()
    {
        if (playerTransform == null) return;

        Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;
        transform.position += directionToPlayer * moveSpeed * Time.deltaTime;
    }

    void OnDestroy()
    {
        WaveSpawner waveSpawner = FindObjectOfType<WaveSpawner>();

        if (waveSpawner != null)
        {
            waveSpawner.EnemyDestroyed(gameObject);
        }
    }

}
