using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float maxFloatDistance = 10f;
    public float moveSpeed = 2f;
    public float arrivalDistance = 0.5f;
    public float maxAngularVelocity = 100f;
    public float angularDamping = 0.1f;

    private Vector3 targetPoint;
    private float currentAngularVelocity;

    void Start()
    {
        ChooseNewTargetPoint();
        currentAngularVelocity = Random.Range(-maxAngularVelocity, maxAngularVelocity);
    }

    void Update()
    {
        AsteroidMovement();
        ApplyAngularDamping();
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

    private void ApplyAngularDamping()
    {
        currentAngularVelocity = Mathf.Lerp(currentAngularVelocity, 0f, angularDamping * Time.deltaTime);
        transform.Rotate(0, 0, currentAngularVelocity * Time.deltaTime);
    }

    private void ChooseNewTargetPoint()
    {
        float randomX = Random.Range(-maxFloatDistance, maxFloatDistance);
        float randomY = Random.Range(-maxFloatDistance, maxFloatDistance);

        targetPoint = new Vector3(transform.position.x + randomX, transform.position.y + randomY, transform.position.z);
    }
}