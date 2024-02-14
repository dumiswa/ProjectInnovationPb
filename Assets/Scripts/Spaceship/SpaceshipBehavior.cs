using System.Collections;
using System.Collections.Generic;
using UnityEditor.TextCore.Text;
using UnityEngine;
using UnityEngine.UIElements;

public class SpaceshipBehavior : MonoBehaviour
{
    public float baseSpeed = 10f;
    public float boostMultiplier = 2f;
    private Rigidbody rb;
    public bool isBoosting;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        HandleMovement();
    }

    void FixedUpdate()
    {      
       
    }

    void HandleMovement()
    {
        isBoosting = Input.GetKey(KeyCode.Space);

        float speed = baseSpeed * (isBoosting ? boostMultiplier : 1f);

        Vector3 force = transform.forward * speed;
        rb.AddForce(force, ForceMode.Force);

        //rb.velocity = Vector3.ClampMagnitude(rb.velocity, speed);
        Debug.Log($"Is Boosting: {isBoosting}");
        Debug.Log($"Speed is: {speed}");
    }
}
