using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectorManager : MonoBehaviour
{
    private List<SpaceshipSelector> selectors = new List<SpaceshipSelector>();
    public GameObject selectorPrefab;
    public static SelectorManager instance;

    public Button PlayButton;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("2 SELECTORMANAGERS IN SCENE, DELETE AT LEAST ONE OF THEM");
        }
    }

    public void ChangeSelection(int playerIndex, int changeID)
    {
        Debug.Log($"{playerIndex} + {changeID}");
        switch (changeID)
        {
            case 0:
                selectors[playerIndex - 1].PrevShip();
                break;
            case 1:
                selectors[playerIndex - 1].NextShip();
                break;
            case 2:
                selectors[playerIndex - 1].ready = !selectors[playerIndex - 1].ready;
                int readyClients = 0;
                foreach (var selector in selectors)
                {
                    if (selector.ready)
                        readyClients++;
                }

                PlayButton.interactable = readyClients == selectors.Count;
                break;
        }
    }

    public void PlayGame()
    {
        StartCoroutine(StartGame());
    }

    public IEnumerator StartGame()
    {
        SERVER.instance.StopAcceptingClients();
        SERVER.instance.ToControllerScreen();
        for (int i = 0; i < selectors.Count; i++)
        {
            SERVER.instance.shipIndices.Add(selectors[i].selectedShip);
        }
        AsyncOperation operation = SceneManager.LoadSceneAsync("ConnectionScene");

        while (!operation.isDone)
        {
            yield return new WaitForSeconds(0.05f);
        }

        SERVER.instance.OnSceneChange();
    }

    public void AddPlayer(int playerIndex)
    {
        GameObject selector = Instantiate(selectorPrefab, new Vector3(0, playerIndex * 20, 0), quaternion.identity);
        selectors.Add(selector.GetComponentInChildren<SpaceshipSelector>());
        SetViewRect();
    }
    
    private void SetViewRect()
    {
        if (selectors.Count <= 2)
        {
            for (int i = 0; i < selectors.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        selectors[i].GetComponentInParent<Camera>().rect = new Rect(new Vector2(0, 0), new Vector2(0.5f, 1));
                        break;
                    case 1:
                        selectors[i].GetComponentInParent<Camera>().rect = new Rect(new Vector2(0.5f, 0), new Vector2(0.5f, 1));
                        break;
                }
                
            }
        }

        if (selectors.Count > 2)
        {
            for (int i = 0; i < selectors.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        selectors[i].GetComponentInParent<Camera>().rect = new Rect(new Vector2(0, 0.5f), new Vector2(0.5f, 0.5f));
                        break;
                    case 1:
                        selectors[i].GetComponentInParent<Camera>().rect = new Rect(new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f));
                        break;
                    case 2:
                        selectors[i].GetComponentInParent<Camera>().rect = new Rect(new Vector2(0, 0), new Vector2(0.5f, 0.5f));
                        break;
                    case 3:
                        selectors[i].GetComponentInParent<Camera>().rect = new Rect(new Vector2(0.5f, 0), new Vector2(0.5f, 0.5f));
                        break;
                }
                
            }
        }
    }
}