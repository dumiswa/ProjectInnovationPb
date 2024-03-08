using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public List<GameObject> playerPrefab;

    public List<Transform> spawnPoints;

    private List<GameObject> players = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("2 GAMEMANAGERS IN SCENE, DELETE AT LEAST ONE OF THEM");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddNewPlayer(int playerIndex, int shipIndex)
    {
        GameObject player = Instantiate(playerPrefab[shipIndex], spawnPoints[playerIndex - 1].position, spawnPoints[playerIndex - 1].rotation);
        players.Add(player);
        player.GetComponent<Prototype>().Init(playerIndex);
        SetViewRect();
        InputManager.instance.InitializePlayerInput(playerIndex);
    }

    private void SetViewRect()
    {
        if (players.Count == 1)
        {
            players[0].GetComponentInChildren<Camera>().rect = new Rect(new Vector2(0, 0), new Vector2(1, 1));
        }
        
        if (players.Count == 2)
        {
            for (int i = 0; i < players.Count; i++)
            {
                switch (i)
                {
                   case 0:
                       players[i].GetComponentInChildren<Camera>().rect = new Rect(new Vector2(0, 0), new Vector2(0.5f, 1));
                       break;
                   case 1:
                       players[i].GetComponentInChildren<Camera>().rect = new Rect(new Vector2(0.5f, 0), new Vector2(0.5f, 1));
                       break;
                }
                
            }
        }

        if (players.Count > 2)
        {
            for (int i = 0; i < players.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        players[i].GetComponentInChildren<Camera>().rect = new Rect(new Vector2(0, 0.5f), new Vector2(0.5f, 0.5f));
                        break;
                    case 1:
                        players[i].GetComponentInChildren<Camera>().rect = new Rect(new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f));
                        break;
                    case 2:
                        players[i].GetComponentInChildren<Camera>().rect = new Rect(new Vector2(0, 0), new Vector2(0.5f, 0.5f));
                        break;
                    case 3:
                        players[i].GetComponentInChildren<Camera>().rect = new Rect(new Vector2(0.5f, 0), new Vector2(0.5f, 0.5f));
                        break;
                }
                
            }
        }
    }
}
