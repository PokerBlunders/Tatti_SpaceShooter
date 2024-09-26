using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public Transform[] waypoints;
    public float targetSpeed = 3f;
    public float timeToReachTargetSpeed = 2f;
    public float stoppingDistance = 0.1f;

    private int currentWaypointIndex = 0;
    private float acceleration = 0f;
    private Vector3 velocity = Vector3.zero;
    public float decelerationDistance = 1.0f;

    void Start()
    {
        acceleration = targetSpeed / timeToReachTargetSpeed;
    }

    void Update()
    {
        EnemyMovement();
    }

    public void EnemyMovement()
    {
        if (waypoints.Length == 0) return;

        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector3 directionToTarget = (targetWaypoint.position - transform.position).normalized;

        float distanceToTarget = Vector3.Distance(transform.position, targetWaypoint.position);

        float speedFactor = Mathf.Clamp(distanceToTarget / decelerationDistance, 0.1f, 1.0f);
        float currentSpeed = targetSpeed * speedFactor;

        velocity += directionToTarget * acceleration * Time.deltaTime;

        if (velocity.magnitude > currentSpeed)
        {
            velocity = velocity.normalized * currentSpeed;
        }

        transform.position += velocity * Time.deltaTime;

        if (distanceToTarget < stoppingDistance)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            velocity = Vector3.zero;
        }
    }

}
