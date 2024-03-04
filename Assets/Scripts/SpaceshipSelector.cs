using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpaceshipSelector : MonoBehaviour
{
    public GameObject[] ships;
    public int selectedShip;

    public void NextShip()
    {
        ships[selectedShip].SetActive(false);
        selectedShip = (selectedShip + 1) % ships.Length;
        ships[selectedShip].SetActive(true);
    }

    public void PrevShip()
    {
        ships[selectedShip].SetActive(false);
        selectedShip--;
        if(selectedShip < 0)
        {
            selectedShip += ships.Length;
        }
        ships[selectedShip].SetActive(true);
    }

    public void StartGame()
    {
        PlayerPrefs.SetInt("selectedShip", selectedShip);
        SceneManager.LoadScene("OutdoorsScene");
    }
}
