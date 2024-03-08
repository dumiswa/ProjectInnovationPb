using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLights : MonoBehaviour
{
    public Color redColor = Color.red;
    public Color greenColor = Color.green;

    public Light[] pointLights;
    public CheckPoint race;

    public void Start()
    {
        race = FindObjectOfType<CheckPoint>();
    }

    private void Update()   
    {   
        if (race.raceStarted)
        {
            SetColor(greenColor);
        }
        else
        {
            SetColor(redColor);
        }
    }

    public void SetColor(Color color)
    {
        foreach (Light light in pointLights)
        {
            light.color = color;
        }
    }
}
