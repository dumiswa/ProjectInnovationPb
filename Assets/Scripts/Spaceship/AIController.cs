using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public List<Transform> waypoints;
    public float movementSpeed = 100f;
    private int currentWaypointIndex = 0;

    void Update()
    {
        if( currentWaypointIndex < waypoints.Count )
        {
            Transform targetWaypoint = waypoints[currentWaypointIndex];
            Vector3 moveDirection = (targetWaypoint.position - transform.position).normalized;
            transform.position += moveDirection * movementSpeed * Time.deltaTime;

            float distanceToWaypoint = Vector3.Distance(transform.position, targetWaypoint.position);
            if(distanceToWaypoint < 1f)
            {
                currentWaypointIndex++;
            }
        }
    }
}
