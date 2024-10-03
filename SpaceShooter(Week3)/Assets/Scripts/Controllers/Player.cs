using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<Transform> asteroidTransforms;
    public Transform enemyTransform;
    public GameObject bombPrefab;
    public Transform bombsTransform;

    public float targetSpeed = 3f;
    public float timeToReachTargetSpeed = 2f;
    
    private float acceleration = 1f;
    private Vector3 velocity = Vector3.zero;

    public float maxSpeed;
    public float accelerationTime;

    public float decelerationTime = 2f;

    public float radarRadius = 5f;
    public int circlePoints = 8;

    public GameObject powerupPrefab;
    public float powerupRadius = 3f;
    public int numberOfPowerups = 5;


    private void Start()
    {
        acceleration = targetSpeed / timeToReachTargetSpeed;

        List<string> words = new List<string>();
        words.Add("Dog");
        //Dog[0]
        words.Add("Cat");
        //Dog[0], Cat[1]
        words.Add("Log");
        //Dog[0], Cat[1], Log[2]

        words.Insert(1, "Rat");
        //Dog[0], Rat[1], Cat[2], Log[3]

        words.Remove("Dog");
        //Rat[0], Cat[1], Log[2]

        Debug.Log("Index of the cat is:" + words.IndexOf("Cat"));

        /*for(int i = 0; i < words.Count; i++)
        {
            Debug.Log(words[i]);
        }*/
        foreach(string word in words)
        {
            Debug.Log(word);
        }

        SpawnPowerups(powerupRadius, numberOfPowerups);

    }

    void Update()
    {
        PlayerMovement();
        EnemyRadar(radarRadius, circlePoints);

        if (Input.GetKeyDown("w")){Debug.Log("Speed =" + velocity.magnitude);}
    }

    public void PlayerMovement()
    {
        bool isMoving = false;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            velocity += Vector3.left * acceleration * Time.deltaTime;
            isMoving = true;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            velocity += Vector3.right * acceleration * Time.deltaTime;
            isMoving = true;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            velocity += Vector3.up * acceleration * Time.deltaTime;
            isMoving = true;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            velocity += Vector3.down * acceleration * Time.deltaTime;
            isMoving = true;
        }

        if (velocity.magnitude > maxSpeed)
        {
            velocity = velocity.normalized * maxSpeed;
        }

        if (!isMoving)
        {
            velocity = Vector3.Lerp(velocity, Vector3.zero, Time.deltaTime / decelerationTime);
        }

        acceleration = maxSpeed / accelerationTime;

        transform.position += velocity * Time.deltaTime;
    }

    public void EnemyRadar(float radius, int circlePoints)
    {
        float angleNext = 360f / circlePoints;

        float distanceToEnemy = Vector3.Distance(transform.position, enemyTransform.position);
        bool isEnemyInRange = distanceToEnemy <= radius;
        Color circleColor;

        if (isEnemyInRange) 
        {
            circleColor = Color.red;
        }
        else
        {
            circleColor = Color.green;
        }

        for (int i = 0; i < circlePoints; i++)
        {
            float angle1 = i * angleNext * Mathf.Deg2Rad;
            float angle2 = (i + 1) * angleNext * Mathf.Deg2Rad;

            Vector3 point1 = new Vector3(Mathf.Cos(angle1), Mathf.Sin(angle1)) * radius + transform.position;
            Vector3 point2 = new Vector3(Mathf.Cos(angle2), Mathf.Sin(angle2)) * radius + transform.position;

            Debug.DrawLine(point1, point2, circleColor);
        }
    }

    public void SpawnPowerups(float radius, int numberOfPowerups)
    {
        float angleNext = 360f / numberOfPowerups;

        for (int i = 0; i < numberOfPowerups; i++)
        {
            float angle = i * angleNext * Mathf.Deg2Rad;
            Vector3 spawnPosition = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)) * radius + transform.position;
            Instantiate(powerupPrefab, spawnPosition, Quaternion.identity);
        }
    }

}
