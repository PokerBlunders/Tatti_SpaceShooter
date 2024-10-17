using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    public float initialSpeed = 2f;
    public float maxSpeed = 10f;
    public float accelerationRate = 0.5f;
    public float rotateSpeed = 200f;
    public GameObject target;
    public float collisionDistance = 0.5f;
    public float targetSearchRadius = 15f;

    float currentSpeed;
    WaveSpawner waveSpawner;
    bool lockedOn = false;

    void Start()
    {
        currentSpeed = initialSpeed;
        waveSpawner = FindObjectOfType<WaveSpawner>();
    }

    void Update()
    {
        if (target != null && lockedOn)
        {
            HomingMovement();
            CheckCollision();
        }
        else
        {
            StraightMovement();
            if (!lockedOn)
            {
                SearchForTarget();
            }
        }
    }

    void HomingMovement()
    {
        currentSpeed = Mathf.Min(currentSpeed + accelerationRate * Time.deltaTime, maxSpeed);

        Vector3 direction = (target.transform.position - transform.position).normalized;
        float rotate = rotateSpeed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.up, direction, rotate, 0.0f);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, newDirection);
        transform.position += transform.up * currentSpeed * Time.deltaTime;
    }

    void StraightMovement()
    {
        currentSpeed = Mathf.Min(currentSpeed + accelerationRate * Time.deltaTime, maxSpeed);
        transform.position += transform.up * currentSpeed * Time.deltaTime;

        Vector3 onScreen = Camera.main.WorldToViewportPoint(transform.position);

        if (onScreen.x < 0 || onScreen.x > 1 || onScreen.y < 0 || onScreen.y > 1)
        {
            Destroy(gameObject);
        }
    }

    void CheckCollision()
    {
        if (target != null && Vector3.Distance(transform.position, target.transform.position) < collisionDistance)
        {
            Destroy(target);
            Destroy(gameObject);
        }
    }

    void SearchForTarget()
    {
        if (waveSpawner == null) return;

        List<GameObject> enemies = waveSpawner.GetActiveEnemies();
        float closestDistance = targetSearchRadius;
        GameObject closestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < closestDistance)
            {
                closestEnemy = enemy;
                closestDistance = distanceToEnemy;
            }
        }

        if (closestEnemy != null)
        {
            target = closestEnemy;
            lockedOn = true;
        }
    }
}
