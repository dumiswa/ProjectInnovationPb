using UnityEngine;

public class CheckPointTrigger : MonoBehaviour
{
    public int checkpointIndex; // Assign this in the inspector
    public float rotationSpeed = 30f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CheckPoint.Instance.CheckpointPassed(checkpointIndex);
        }
    }

    private void Update()
    {
        transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
    }
}