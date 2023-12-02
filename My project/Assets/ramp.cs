using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ramp : MonoBehaviour
{
    public Transform car;
    public Transform end;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(car.position, end.position);
        if (distance < 3)
        {
            car.eulerAngles = new Vector3(car.rotation.x, car.rotation.y, 0);
        }
    }
}
