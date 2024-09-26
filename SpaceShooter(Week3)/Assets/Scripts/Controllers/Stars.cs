using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour
{
    public List<Transform> starTransforms;
    public float drawingTime = 1f;

    private int currentStarIndex = 0;
    private float lineDrawProgress = 0f;

    void Start()
    {
        StartCoroutine(DrawConstellation());
    }

    public IEnumerator DrawConstellation()
    {
        while (true)
        {
            for (int i = 0; i < starTransforms.Count; i++)
            {
                Transform currentStar = starTransforms[i];
                Transform nextStar = starTransforms[(i + 1) % starTransforms.Count];

                lineDrawProgress = 0f;

                while (lineDrawProgress < 1f)
                {
                    lineDrawProgress += Time.deltaTime / drawingTime;

                    Vector3 interpolatedPosition = Vector3.Lerp(currentStar.position, nextStar.position, lineDrawProgress);

                    Debug.DrawLine(currentStar.position, interpolatedPosition, Color.white);

                    yield return null;
                }

                Debug.DrawLine(currentStar.position, nextStar.position, Color.white);

                yield return null;
            }
        }
    }
}
