using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICarController : MonoBehaviour
{
    public List<Transform> waypoints;
    public float moveSpeed = 10.0f;
    public float maxSteerAngle = 30.0f;
    public float lookAheadDistance = 5.0f;

    public WheelCollider[] frontWheelColliders;
    public WheelCollider[] rearWheelColliders;
    public WheelCollider[] allWheelColliders;
    public Transform[] wheelMeshes;

    private int currentWaypointIndex = 0;
    private float smoothSteerAngleVelocity;
    
    private void Update()
    {
        if (waypoints.Count == 0)
        {
            Debug.LogError("No waypoints assigned to AI car.");
            return;
        }

        // Check if we have reached the current waypoint.
        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 5.0f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Count)
            {
                currentWaypointIndex = 0; // Loop back to the first waypoint.
            }
        }

        // Calculate the direction to the current waypoint.
        Vector3 targetDirection = (waypoints[currentWaypointIndex].position - transform.position).normalized;

        // Calculate steering angle based on the direction to the waypoint with look-ahead.
        float steerAngle = Vector3.SignedAngle(transform.forward, targetDirection, Vector3.up);
        float distanceToWaypoint = Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position);

        // Smooth the steering angle change based on distance to the waypoint and look-ahead distance.
        float smoothFactor = Mathf.Clamp01(distanceToWaypoint / lookAheadDistance);
        steerAngle = Mathf.SmoothDamp(frontWheelColliders[0].steerAngle, steerAngle, ref smoothSteerAngleVelocity, 0.2f * smoothFactor);

        // Apply steering to the front wheel colliders.
        for (int i = 0; i < frontWheelColliders.Length; i++)
        {
            frontWheelColliders[i].steerAngle = steerAngle;
        }

        // Apply driving force to the rear wheel colliders.
        for (int i = 0; i < rearWheelColliders.Length; i++)
        {
            rearWheelColliders[i].motorTorque = moveSpeed;
        }

        // Rotate wheel meshes smoothly.
        for (int i = 0; i < wheelMeshes.Length; i++)
        {
            Quaternion rotation;
            Vector3 position;
            allWheelColliders[i].GetWorldPose(out position, out rotation);
            wheelMeshes[i].rotation = Quaternion.Slerp(wheelMeshes[i].rotation, rotation, Time.deltaTime * 5);
        }
    }
}







