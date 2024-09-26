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

        velocity += directionToTarget * acceleration * Time.deltaTime;

        if (velocity.magnitude > targetSpeed)
        {
            velocity = velocity.normalized * targetSpeed;
        }

        transform.position += velocity * Time.deltaTime;

        if (Vector3.Distance(transform.position, targetWaypoint.position) < stoppingDistance)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            velocity = Vector3.zero;
        }
    }

}
