using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour
{
    public List<Transform> starTransforms;
    public float drawingTime = 1f;

    private int currentStarIndex = 0;
    private float lineDrawProgress = 0f;

    void Update()
    {
        DrawConstellation();
    }

    public void DrawConstellation()
    {
        Transform currentStar = starTransforms[currentStarIndex];
        Transform nextStar = starTransforms[(currentStarIndex + 1) % starTransforms.Count];

        lineDrawProgress += Time.deltaTime / drawingTime;

        Vector3 interpolatedPosition = Vector3.Lerp(currentStar.position, nextStar.position, lineDrawProgress);

        Debug.DrawLine(currentStar.position, interpolatedPosition, Color.white);

        if (lineDrawProgress >= 1f)
        {
            currentStarIndex = (currentStarIndex + 1) % starTransforms.Count;
            lineDrawProgress = 0f;
        }
    }
}
