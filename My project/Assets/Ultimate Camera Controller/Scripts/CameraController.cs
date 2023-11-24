using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target = null;
    public GameObject cameraTarget = null;
    public float followSpeed = 1.5f;
    public Vector3 offset = Vector3.zero;
    public float lookSpeed = 27.0f;
    int viewNumber = 1;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        cameraTarget = GameObject.FindGameObjectWithTag("Target");
    }

    void FixedUpdate()
    {
        // Look at the target (car)
        Vector3 lookDirection = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(lookDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, lookSpeed * Time.deltaTime);

        // Calculate the new position to follow the car
        Vector3 desiredPosition = target.TransformPoint(offset);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            viewNumber++;
        }
        if (viewNumber == 1)
        {
            followSpeed =2.2f;
            offset = new Vector3(0,3,-4);
            lookSpeed = 27;

        }
        else if (viewNumber == 2)
        {
            followSpeed = 999;
            // Position the camera on the car's front hood
            offset = new Vector3(0, 0.3f, 1.2f);
            transform.rotation = target.rotation; // Align with the car's rotation
            lookSpeed = 99999;
        }
        if (viewNumber == 3)
        {
            followSpeed = 999;
            transform.rotation = target.rotation;
            offset = new Vector3(0,1, -3);
            lookSpeed = 99999;
        }
        if (viewNumber ==4)
        {
            viewNumber = 1;

        }
    }
}