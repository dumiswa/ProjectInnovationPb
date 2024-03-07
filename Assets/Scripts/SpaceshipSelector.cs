using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpaceshipSelector : MonoBehaviour
{
    public GameObject ShipParent;
    private Vector3 position2P = new Vector3(1, 8.8f, 105.8f);
    private Vector3 position4P = new Vector3(-29.5f, 18.1f, 96.49886f);
    private Vector3 rotation2P = new Vector3(-3.278f, 0, 0);
    private Vector3 rotation4P = new Vector3(-12.47f, -12.47f, -4.877f);
    public GameObject[] ships;
    public int selectedShip;
    public string shipName;
    public TextMeshProUGUI nameText;

    public GameObject UI2P;
    public GameObject UI4P;

    public bool ready;

    public void NextShip()
    {
        ships[selectedShip].SetActive(false);
        selectedShip = (selectedShip + 1) % ships.Length;
        ships[selectedShip].SetActive(true);
    }

    public void SetName(string newName)
    {
        shipName = newName;
        if(nameText != null)
            nameText.text = shipName;
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
    }

    public void ChangePlayer(bool p4)
    {
        if (p4)
        {
            UI4P.SetActive(true);
            UI2P.SetActive(false);
            ShipParent.transform.localPosition = position4P;
            ShipParent.transform.localRotation = Quaternion.Euler(rotation4P);
        }
        else
        {
            UI2P.SetActive(true);
            UI4P.SetActive(false);
            ShipParent.transform.localPosition = position2P;
            ShipParent.transform.localRotation = Quaternion.Euler(rotation2P);
        }
    }
}
