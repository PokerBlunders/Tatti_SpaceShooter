using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float maxFloatDistance = 10f;
    public float moveSpeed = 2f;
    public float arrivalDistance = 0.5f;

    private Vector3 targetPoint;

    void Start()
    {
        ChooseNewTargetPoint();
    }

    void Update()
    {
        AsteroidMovement();
    }

    public void AsteroidMovement()
    {
        Vector3 directionToTarget = (targetPoint - transform.position).normalized;
        transform.position += directionToTarget * moveSpeed * Time.deltaTime;

        if (Vector3.Distance(transform.position, targetPoint) < arrivalDistance)
        {
            ChooseNewTargetPoint();
        }
    }

    private void ChooseNewTargetPoint()
    {
        float randomX = Random.Range(-maxFloatDistance, maxFloatDistance);
        float randomY = Random.Range(-maxFloatDistance, maxFloatDistance);

        targetPoint = new Vector3(transform.position.x + randomX, transform.position.y + randomY, transform.position.z);
        //Debug.Log("New target point: " + targetPoint);
    }
}

