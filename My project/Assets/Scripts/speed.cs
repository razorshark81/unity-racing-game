using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class speed : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI speedText;

    [SerializeField]
    private float smoothSpeed = 5.0f; // Adjust the smoothness factor as needed
    [SerializeField] private float offset;
    [SerializeField] private float multiplier;
    private float targetRotationZ;
    private float currentRotationZ;

    void Update()
    {
        // Parse the speed value from the text and convert it to a float
        float speedNumber;
        if (float.TryParse(speedText.text, out speedNumber))
        {
            // Calculate the target rotation for the speedometer needle
            targetRotationZ = (-speedNumber -offset ) * multiplier;

            // Smoothly rotate the speedometer needle
            currentRotationZ = Mathf.Lerp(currentRotationZ, targetRotationZ, Time.deltaTime * smoothSpeed);

            // Apply the rotation to the speedometer needle
            transform.eulerAngles = new Vector3(0, 0, currentRotationZ);
        }
    }
}
