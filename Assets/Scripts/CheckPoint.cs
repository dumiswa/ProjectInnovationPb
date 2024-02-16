using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public static CheckPoint Instance { get; private set; }

    [Header("Checkpoints")]
    public GameObject start;
    public GameObject end;
    public GameObject[] checkPointArray;

    [Header("Laps")]
    public int totalLaps;
    private int currentLap = 0;
    private int currentCheckpointIndex = 0;

    private bool raceStarted = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetRace();
        }
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void ResetRace()
    {
        raceStarted = false;
        currentLap = 0;
        currentCheckpointIndex = 0;
        Debug.Log("Race reset. Press R to start.");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("collided");
            if (!raceStarted && gameObject == start)
            {
                StartRace();
            }
            else if (raceStarted && gameObject == checkPointArray[currentCheckpointIndex])
            {
                AdvanceCheckpoint();
            }
        }
    }

    public void CheckpointPassed(int index)
    {
        if (!raceStarted && index == 0)
        {
            StartRace();
            return; 
        }

        if (!raceStarted) return;

        if (index == currentCheckpointIndex)
        {
            Debug.Log($"Checkpoint {index} passed.");
            currentCheckpointIndex = (currentCheckpointIndex + 1) % checkPointArray.Length;

            if (index == 0)
            {
                CompleteLap();
            }
        }
    }

    private void StartRace()
    {
        raceStarted = true;
        currentLap = 1;
        currentCheckpointIndex = 1; 
        Debug.Log("Race has started!");
    }

    private void AdvanceCheckpoint()
    {
        Debug.Log($"Checkpoint {currentCheckpointIndex} reached.");
        currentCheckpointIndex++;

        if (gameObject == end)
        {
            CompleteLap();
        }
    }

    private void CompleteLap()
    {
        currentLap++;
        Debug.Log($"Lap {currentLap} completed.");

        currentCheckpointIndex = 1;

        if (currentLap > totalLaps)
        {
            EndRace();
        }
    }

    private void EndRace()
    {
        Debug.Log("Race Finished!");
        raceStarted = false;
    }

    private void OnGUI()
    {
        if (raceStarted)
        {
            GUI.Label(new Rect(10, 10, 200, 20), $"Lap: {currentLap}/{totalLaps}");
            GUI.Label(new Rect(10, 30, 200, 20), $"Checkpoint: {currentCheckpointIndex}/{checkPointArray.Length}");
        }
    }
}
