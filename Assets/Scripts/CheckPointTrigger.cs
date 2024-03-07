using UnityEngine;

public class CheckPointTrigger : MonoBehaviour
{
    public int checkpointIndex; // Assign this in the inspector
    public float rotationSpeed = 30f;
    public bool isRotating = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CheckPoint.Instance.CheckpointPassed(checkpointIndex, other.GetComponent<Prototype>().playerIndex);
        }
        else if (other.CompareTag("AI"))
        {
            CheckPoint.Instance.CheckpointPassed(checkpointIndex, other.GetComponent<AIController>().AIIndex);
        }
    }

    private void Update()
    {
        if(isRotating) 
            transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
    }
}