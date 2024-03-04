using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AIController : MonoBehaviour
{
    [Header("Waypoint Navigation")]
    public List<Transform> waypoints;
    public float movementSpeed = 100f;
    private int currentWaypointIndex = 0;

    [Header("Steering Behaviour")]
    public Transform target;
    public float maxSteeringSpeed = 50f;
    public float rotationSpeed = 10f;
    public float maxRollAngle = 10f;
    public float rollAngleMulitplier = 0.5f;
    //public float maxPitchAngle = 10f;
    //public float pitchAngleMultiplier = 0.5f;
    public float maxDeviationAngle = 10f;

    private Vector3 deviationVector;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }


    void Update()
    {
        WaypointNavigation();
        SteeringBehaviour();      
    }


    void WaypointNavigation()
    {
        if (currentWaypointIndex < waypoints.Count)
        {   
            Transform targetWaypoint = waypoints[currentWaypointIndex];

            Vector3 moveDirection = (targetWaypoint.position - transform.position).normalized;

            //float deviationAngle = Random.Range(-maxDeviationAngle, maxDeviationAngle);
            //moveDirection = Quaternion.Euler(0f, deviationAngle, 0f) * moveDirection;

            transform.position += moveDirection * movementSpeed * Time.deltaTime;

            float distanceToWaypoint = Vector3.Distance(transform.position, targetWaypoint.position);
            if (distanceToWaypoint < 1f)
            {
                currentWaypointIndex++;
                CalculateDeviationAngle();
            }
        }
    }

    void CalculateDeviationAngle()
    {
        deviationVector = Random.insideUnitCircle * maxDeviationAngle;
        Debug.Log("Deviation Vector" + deviationVector);
    }

    void SteeringBehaviour()
    {
        if (target == null)
            return;

        target = waypoints[currentWaypointIndex];

        // Calculates the distance to the target and rotates the GameObject towards it
        Vector3 distanceToTarget = (target.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(distanceToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Calculates the needed amount of velocity and steering force and applies it 
        Vector3 desiredVelocity = distanceToTarget * maxSteeringSpeed;
        Vector3 steeringForce = desiredVelocity - rb.velocity;
        rb.AddForce(steeringForce, ForceMode.Acceleration);

        float angleDiff = Vector3.SignedAngle(transform.forward, distanceToTarget, Vector3.up);
        float distanceToCheckpoint = Vector3.Distance(transform.position, target.position);     
        float rollAngle = Mathf.Clamp((angleDiff) * rollAngleMulitplier, -maxRollAngle, maxRollAngle) * Mathf.Clamp01(distanceToCheckpoint);
        //float altitudeDiff = target.position.y - transform.position.y;
        //float targetPitchAngle = Mathf.Clamp(altitudeDiff * pitchAngleMultiplier, -maxPitchAngle, maxPitchAngle) * Mathf.Clamp01(distanceToCheckpoint);
           
        Quaternion rollRotation = Quaternion.Euler(0f, 0f, -rollAngle);
        transform.rotation *= rollRotation;

        //Quaternion pitchRotation = Quaternion.Euler(targetPitchAngle, 0f, 0f);
        //transform.rotation *= pitchRotation;
    } 

    private void OnDrawGizmos()
    {
        // Draw a green line from the object's position to the deviation vector
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, target.position);
    }
}
