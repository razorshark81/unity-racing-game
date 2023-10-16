using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AICarController : MonoBehaviour
{
    public List<Transform> waypoints;
    public float moveSpeed = 10.0f;
    public float maxSteerAngle = 30.0f;
    public float lookAheadDistance = 5.0f;
    public LayerMask obstacleLayer; // Define the layer for road barriers.
    public LayerMask carsLayer;

    public WheelCollider[] frontWheelColliders;
    public WheelCollider[] rearWheelColliders;
    public WheelCollider[] allWheelColliders;
    public Transform[] wheelMeshes;

    private int currentWaypointIndex = 0;
    private float smoothSteerAngleVelocity;
    public GameObject waypointParent;
    public float obstacleSlowdown;
    public float originalMoveSpeed;
    public float rotationSmoothness;
    public float maxSteerAngleThreshold;
    public float maxSteerSpeedReduction;
    private float currentSteerAngle;
    public float maxWaypointOffset = 2.0f; // Adjust the maximum offset distance as needed.
    public float sharpTurnLookahead;
    public float maxTurnAngle;



    private void Start()
    {
        Transform path = waypointParent.transform;
        for (int i = 0; i < path.childCount; i++)
        {
            waypoints.Add(path.GetChild(i).transform);
        }
    }

    private void Update()
    {



         
        

        if (waypoints.Count == 0)
        {
            Debug.LogError("No waypoints assigned to AI car.");
            return;
        }
        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 10.0f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Count)
            {
                currentWaypointIndex = 0; // Loop back to the first waypoint.
            }
        }

        // Calculate the direction to the current waypoint.
        Vector3 targetDirection = (waypoints[currentWaypointIndex].position - transform.position).normalized;
        Vector3 offset = new Vector3(Random.Range(-maxWaypointOffset, maxWaypointOffset), 0, Random.Range(-maxWaypointOffset, maxWaypointOffset));
        Vector3 newWaypointPosition = waypoints[currentWaypointIndex].position + offset;

        // Calculate the direction to the new waypoint position.
        targetDirection = (newWaypointPosition - transform.position).normalized;

        // Check for obstacles in front of the car.
        // Center ray
        bool obstacleDetected = RaycastForCenterObstacle(0);
        bool leftObstacleDetected = RaycastForObstacle(-45); // Left ray
        bool rightObstacleDetected = RaycastForObstacle(45); // Right ray
        bool carDetected = RaycastForCars(0);
        if (carDetected)
        {
            if (leftObstacleDetected&&!rightObstacleDetected)
            {
                Steer(maxSteerAngle);
                
                moveSpeed = originalMoveSpeed + 200;
            }
            if (rightObstacleDetected && !leftObstacleDetected)
            {
                Steer(-maxSteerAngle);
                moveSpeed = originalMoveSpeed +200;
                
            }
            else if (!leftObstacleDetected && !rightObstacleDetected) {
                Steer(-maxSteerAngle);
                moveSpeed = originalMoveSpeed + 200;
                
            }
            
        }


        if (leftObstacleDetected && !rightObstacleDetected)
        {
            Steer(maxSteerAngle); // Steer right to avoid left obstacle
            
            moveSpeed = originalMoveSpeed;
        }
        else if (!leftObstacleDetected && rightObstacleDetected)
        {
            Steer(-maxSteerAngle); // Steer left to avoid right obstacle
            ;
            moveSpeed = originalMoveSpeed;
        }
        else if (leftObstacleDetected && rightObstacleDetected)
        {
            // Obstacle detected but no clear path; you can add custom logic here
            
            float steerAngle = Vector3.SignedAngle(transform.forward, targetDirection, Vector3.up);
            steerAngle = Mathf.Clamp(steerAngle, -maxSteerAngle, maxSteerAngle);
            Steer(steerAngle);
            
        }
        
        else
        {
            // No obstacle detected, continue waypoint following.
            float steerAngle = Vector3.SignedAngle(transform.forward, targetDirection, Vector3.up);
            steerAngle = Mathf.Clamp(steerAngle, -maxSteerAngle, maxSteerAngle);
            Steer(steerAngle);
            moveSpeed = originalMoveSpeed;
        }
        if (obstacleDetected)
        {
            moveSpeed = obstacleSlowdown;
            
        }
        currentSteerAngle = Vector3.SignedAngle(transform.forward, targetDirection, Vector3.up);
        currentSteerAngle = Mathf.Abs(currentSteerAngle);
        if (currentSteerAngle > maxSteerAngleThreshold)
        {
            // Reduce speed during sharp turns.
            moveSpeed *= maxSteerSpeedReduction;
        }
        if (currentSteerAngle < -maxSteerAngleThreshold)
        {
            // Reduce speed during sharp turns.
            moveSpeed *= maxSteerSpeedReduction;
        }

        Drive();

        // Rotate wheel meshes smoothly.
        for (int i = 0; i < wheelMeshes.Length; i++)
        {
            Quaternion rotation;
            Vector3 position;
            allWheelColliders[i].GetWorldPose(out position, out rotation);
            wheelMeshes[i].rotation = Quaternion.Slerp(wheelMeshes[i].rotation, rotation, Time.deltaTime * 5);
        }
        Vector3 lookaheadPosition = transform.position + transform.forward * sharpTurnLookahead;

        // Check for sharp turns by comparing the target direction with the direction to the lookahead position.
        float turnAngle = Vector3.Angle(targetDirection, lookaheadPosition - transform.position);

        // Automatic speed reduction for anticipated sharp turns.
        if (turnAngle > maxTurnAngle)
        {
            // Reduce speed before a sharp turn.
            moveSpeed *= maxSteerSpeedReduction;
            print("Slowing Down");
        }

    }

    private bool RaycastForObstacle(float angle)
    {
        RaycastHit hit;
        Vector3 rayDirection = Quaternion.Euler(0, angle, 0) * transform.forward;
        Debug.DrawRay(transform.position + new Vector3(0, 1, 0), rayDirection * 7, Color.green);
        return Physics.Raycast(transform.position + new Vector3(0, 1, 0), rayDirection, out hit, 7, obstacleLayer);
        
    }
    private bool RaycastForCars(float angle)
    {
        RaycastHit hit;
        Vector3 rayDirection = Quaternion.Euler(0, angle, 0) * transform.forward;
        Debug.DrawRay(transform.position + new Vector3(0, 1, 0), rayDirection * 10, Color.red);
        return Physics.Raycast(transform.position + new Vector3(0, 1, 0), rayDirection, out hit, 10, carsLayer);
    }
    private bool RaycastForCenterObstacle(float angle)
    {
        RaycastHit hit;
        Vector3 rayDirection = Quaternion.Euler(0, angle, 0) * transform.forward;
        Debug.DrawRay(transform.position + new Vector3(0, 1, 0), rayDirection * 13, Color.green);
        return Physics.Raycast(transform.position + new Vector3(0, 1, 0), rayDirection, out hit, 13, obstacleLayer);

    }

    private void Steer(float  steerAngle)
    {
        steerAngle = Mathf.Clamp(steerAngle, -maxSteerAngle, maxSteerAngle);
        float smoothFactor = Mathf.Clamp01(Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) / lookAheadDistance);
        if (moveSpeed >= 0)
        {
            steerAngle = Mathf.SmoothDamp(frontWheelColliders[0].steerAngle, steerAngle, ref smoothSteerAngleVelocity, 0.2f * smoothFactor);
        }
        steerAngle = Mathf.Clamp(steerAngle, -maxSteerAngle, maxSteerAngle);
        if (moveSpeed <= 0)
        {
            steerAngle = -steerAngle;
        }
        for (int i = 0; i < frontWheelColliders.Length; i++)
        {
            
            frontWheelColliders[i].steerAngle = steerAngle;

        }       

    }

    private void Drive()
    {

        for (int i = 0; i < rearWheelColliders.Length; i++)
        {
            rearWheelColliders[i].motorTorque = moveSpeed;
        }
    }
}
