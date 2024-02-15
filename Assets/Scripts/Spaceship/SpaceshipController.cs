using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipController : MonoBehaviour
{ 
    public float neutralX;
    private bool isCalibrated = false;

    public float rotationSpeedMultiplier = 2.0f;

    private Quaternion initialAttitude;

    void Start()
    {
        Input.gyro.enabled = true;
        CalibrateGyro();
    }

    void Update()
    {
        Quaternion gyro = Input.gyro.attitude;
        Quaternion relativeAttitude = Quaternion.Inverse(initialAttitude) * gyro;
        Vector3 gyroEularAngles = gyro.eulerAngles;

        float zRotation = gyro.eulerAngles.z;
        float yRotation = gyro.eulerAngles.y * rotationSpeedMultiplier;
        float xTilt = gyro.eulerAngles.x;

        transform.eulerAngles = new Vector3(yRotation, 0, zRotation);
        //transform.rotation = Quaternion.Euler(0, transform.rotation.y - zRotation, zRotation);

        /* if (!isCalibrated) return;

         Quaternion gyro = Input.gyro.attitude;
         Vector3 gyroEulerAngles = gyro.eulerAngles;

         float zRotation = gyroEulerAngles.z;
         float xTilt = gyroEulerAngles.x;

         // Adjust the xTilt value to be relative to the neutral position
         if (xTilt > 180) xTilt -= 360; // Normalize xTilt to -180 to 180 range
         float adjustedXTilt = xTilt - neutralX; // Calculate adjusted tilt

         // Map adjustedXTilt to Y position, ensuring the cube's Y is at its original when the phone is level
         float yPos = Mathf.Clamp(adjustedXTilt, -10f, 10f); // Clamp Y position for safety

         // Apply rotation and adjusted Y position
         transform.eulerAngles = new Vector3(0, 0, zRotation);
         transform.position = new Vector3(transform.position.x, yPos, transform.position.z);*/
    }


    void CalibrateGyro()
    {
        initialAttitude = Input.gyro.attitude;
    }    
}
