using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Moon : MonoBehaviour
{
    public Transform planetTransform;
    public float orbitalRadius = 5f;
    public float orbitalSpeed = 1f;
    private float angle = 0f;

    void Update()
    {
        OrbitalMotion(orbitalRadius, orbitalSpeed, planetTransform);
    }

    public void OrbitalMotion(float radius, float speed, Transform planetTransform)
    {
        if (planetTransform == null) return;
        
        angle += speed * Time.deltaTime;
        if (angle > Mathf.PI * 2) angle -= Mathf.PI * 2;

        float xPosition = Mathf.Cos(angle) * radius + planetTransform.position.x;
        float yPosition = Mathf.Sin(angle) * radius + planetTransform.position.y;

        transform.position = new Vector3(xPosition, yPosition, transform.position.z);
    }
}
