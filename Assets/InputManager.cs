using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public List<InputData> inputData = new List<InputData>(4);
    
    public static InputManager instance;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("2 INPUTMANAGERS IN SCENE, DELETE AT LEAST ONE OF THEM");
        }

        for (int i = 0; i < inputData.Capacity; i++)
        {
            inputData.Add(new InputData());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializePlayerInput(int playerIndex)
    {
        inputData[playerIndex - 1] = new InputData();
    }

    public void SetInput(int shipIndex, float elevation, float rotation, bool brake)
    {
        inputData[shipIndex - 1].Elevation = elevation;
        inputData[shipIndex - 1].Rotation = rotation;
        inputData[shipIndex - 1].Brake = brake;
    }

    public InputData GetInput(int shipIndex)
    {
        return inputData[shipIndex - 1];
    }

    public class InputData
    {
        public float Elevation;
        public float Rotation;
        public bool Brake;
    }
}
