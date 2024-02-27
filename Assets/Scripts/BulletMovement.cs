using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    private Vector3 initialVelocity;
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void SetInitialVelocity(Vector3 velocity, Vector3 forwardDirection)
    {
        // If the ship is not moving, use the forward direction for the bullet
        if (velocity == Vector3.forward * 20f) // Assuming 20f is your stationary speed
        {
            rb.velocity = forwardDirection * 20f; // Replace 20f with your desired bullet speed when stationary
        }
        else
        {
            rb.velocity = velocity;
        }
    }

    void Update()
    {

    }
}
