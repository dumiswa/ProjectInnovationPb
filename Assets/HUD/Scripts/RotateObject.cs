using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotationSpeed = 100.0f; // Rotation speed in degrees per second

    // Update is called once per frame
    void Update()
    {
        // Rotate the object around its Y axis at the specified speed
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
