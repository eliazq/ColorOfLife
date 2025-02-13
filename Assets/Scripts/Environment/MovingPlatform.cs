using Invector.vCharacterController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float speed = 3.0f;  // The speed of the platform
    [SerializeField] private Transform pointsParent;  // The parent of the points

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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.grey;
        if (pointsParent != null)
        {
            Gizmos.color = Color.red;

            // Get all child transforms of pointsParent and store their local positions in the points array
            Vector3[] gizmoPoints = new Vector3[pointsParent.childCount];
            for (int i = 0; i < pointsParent.childCount; i++)
            {
                gizmoPoints[i] = pointsParent.GetChild(i).position;
            }

            // Draw spheres at each point
            for (int i = 0; i < gizmoPoints.Length; i++)
            {
                Gizmos.DrawSphere(gizmoPoints[i], 0.2f);
            }

            // Draw lines between the points
            for (int i = 0; i < gizmoPoints.Length; i++)
            {
                Vector3 startPosition = gizmoPoints[i];
                Vector3 endPosition = gizmoPoints[(i + 1) % gizmoPoints.Length];
                Gizmos.DrawLine(startPosition, endPosition);
            }
        }
    }
}
