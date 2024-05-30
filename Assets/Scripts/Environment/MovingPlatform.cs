using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 3.0f;  // The speed of the platform
    public Transform pointsParent;  // The parent of the points

    private Vector3[] points;  // Array of points to move between
    private int currentPointIndex = 0;  // Index of the current target point

    public bool isMoving { get; set; } = true;


    void Start()
    {
        // Ensure pointsParent is assigned
        if (pointsParent != null)
        {
            // Get all child transforms of pointsParent and store them in the points array
            points = new Vector3[pointsParent.childCount];
            for (int i = 0; i < pointsParent.childCount; i++)
            {
                points[i] = pointsParent.GetChild(i).position;
            }
        }
        else
        {
            Debug.LogError("Points Parent is not assigned.");
        }

        if (TryGetComponent(out FallingPlatform fallingPlatform))
        {
            fallingPlatform.OnPlatformFall += FallingPlatform_OnPlatformFall;
            fallingPlatform.OnPlatformReset += FallingPlatform_OnPlatformReset;
        }
    }

    private void FallingPlatform_OnPlatformReset(object sender, System.EventArgs e)
    {
        isMoving = true;
    }

    private void FallingPlatform_OnPlatformFall(object sender, System.EventArgs e)
    {
        isMoving = false;
    }

    void Update()
    {
        // Ensure there are points defined
        if (points.Length > 0 && isMoving)
        {
            // Move the platform towards the current target point in world space
            Vector3 targetPosition = points[currentPointIndex];
            
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            // Check if the platform has reached the target point
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                // Increment the point index and loop back to the start if necessary
                currentPointIndex = (currentPointIndex + 1) % points.Length;
            }
        }
    }
}
