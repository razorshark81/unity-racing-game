using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LightController : MonoBehaviour
{
    public Light leftHeadLight;
    public Light rightHeadLight;
    public GameObject leftHeadlightOn;
    public GameObject rightHeadlightOn;
    
    void Start()
    {
        rightHeadLight.enabled = false;
        leftHeadLight.enabled = false;
        leftHeadlightOn.SetActive(false);
        rightHeadlightOn.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.S)) {
            leftHeadLight.enabled = true;
            rightHeadLight.enabled = true;
            leftHeadlightOn.SetActive(true);
            rightHeadlightOn.SetActive(true);

        }
        if (Input.GetKey(KeyCode.W))
        {
            rightHeadLight.enabled = false;
            leftHeadLight.enabled = false;
            leftHeadlightOn.SetActive(false);
            rightHeadlightOn.SetActive(false);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            leftHeadlightOn.SetActive(false);
            rightHeadlightOn.SetActive(false);
            leftHeadLight.enabled = false;
            rightHeadLight.enabled= false;

        }
        
    }
}
