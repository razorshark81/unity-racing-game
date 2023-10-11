//Ultimate Camera Controller - Camera Controller
//This script is responsible for following a target object and adding orbit functionality
//to the object that it is attached to
//To make a camera follow or orbit around a target you just need to attach this script to 
//the object that contains the camera or one of its parents

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Target = null;
    public GameObject T = null;
    public float speed = 1.5f;

    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player");
        T = GameObject.FindGameObjectWithTag("Target");
    }

    void FixedUpdate()
    {
        this.transform.LookAt(Target.transform);
        float car_Move = Mathf.Abs(Vector3.Distance(this.transform.position, T.transform.position) * speed);
        this.transform.position = Vector3.MoveTowards(this.transform.position, T.transform.position, car_Move * Time.deltaTime);
    }

}