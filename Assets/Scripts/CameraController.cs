using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float smothingSpeed = 5f;

    private void LateUpdate()
    {
        if (target == null)       
            return;
        Vector3 desirePosition = target.position;
        transform.position = Vector3.Lerp(transform.position, desirePosition, smothingSpeed * Time.deltaTime);

        Quaternion desiredRotation = target.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, smothingSpeed * Time.deltaTime);        
    }
}
