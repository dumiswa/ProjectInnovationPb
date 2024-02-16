using UnityEngine;

public class CheckPointTrigger : MonoBehaviour
{
    public int checkpointIndex; // Assign this in the inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CheckPoint.Instance.CheckpointPassed(checkpointIndex);
        }
    }
}