using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public enum MenuState
{
    Home,
    Play,
    Tutorial,
    Options
}

public class MainMenuManager : MonoBehaviour
{
    [Header("HomeMenu")]
    public GameObject homeMenu;
    public GameObject playMenu;
    public GameObject tutorialMenu;
    public GameObject optionsMenu;

    [Header("PlayMenu")]
    public GameObject singlePlayerButton;
    public GameObject twoPlayersButton;
    public GameObject fourPlayersButton;

    public UnityEvent<MenuState> menuChangeEvent;

    private MenuState currentState;     

    public void Start()
    {
        //ChangeMenu(MenuState.Home);
        menuChangeEvent = new UnityEvent<MenuState>();
        homeMenu.SetActive(true);
    }

    public void homeMenuOnClick()
    {
        homeMenu.SetActive(true);
        playMenu.SetActive(false);
        tutorialMenu.SetActive(false);
        optionsMenu.SetActive(false);
    }

    public void playMenuOnClick()
    {
        homeMenu.SetActive(false);
        playMenu.SetActive(true);
        tutorialMenu.SetActive(false);
        optionsMenu.SetActive(false);
    }

    public void tutorialMenuOnClick()
    {
        homeMenu.SetActive(false);
        playMenu.SetActive(false);
        tutorialMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    public void optionsMenuOnClick()
    {
        homeMenu.SetActive(false);
        playMenu.SetActive(false);
        tutorialMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void singlePlayerOnClick()
    {
        SceneManager.LoadScene("ShipSelector");
    }
    
    public void MultiPlayerOnClick()
    {
        SceneManager.LoadScene("ShipSelector");
    }

    /*public void ChangeMenu(MenuState newState)
    {
        DeactivateCurrentMenu();

        switch (newState)
        {
            case MenuState.Home:
                homeMenu.SetActive(true);
                break;
            case MenuState.Play:
                playMenu.SetActive(true);
                break;
            case MenuState.Tutorial:
                tutorialMenu.SetActive(true);
                break;
            case MenuState.Options:
                optionsMenu.SetActive(true);
                break;
        }

        currentState = newState;
        menuChangeEvent.Invoke(newState);
    }

    private void DeactivateCurrentMenu()
    {
        homeMenu.SetActive(false);
        playMenu.SetActive(false);
        tutorialMenu.SetActive(false);
        optionsMenu.SetActive(false);
    }*/
}
