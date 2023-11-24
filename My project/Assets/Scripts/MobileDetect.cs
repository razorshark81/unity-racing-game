using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MobileDetect : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] touchControls;
    
    void Start()
    {
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            for (int i = 0; i < touchControls.Length; i++) 
            {
                touchControls[i].gameObject.SetActive(false);       

            }

        }
        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            for (int i = 0; i < touchControls.Length; i++)
            {
                touchControls[i].gameObject.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
