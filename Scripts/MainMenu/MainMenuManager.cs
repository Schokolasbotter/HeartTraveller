using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{

    //Menu Objects
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject controlsMenu;
    [SerializeField]
    private GameObject creditsMenu;

    public void openMainMenu()
    {
        mainMenu.SetActive(true);
        controlsMenu.SetActive(false);
        creditsMenu.SetActive(false);
    }

    public void openControlsMenu()
    {
        controlsMenu.SetActive(true);
        mainMenu.SetActive(false);
        creditsMenu.SetActive(false);
    }

    public void openCreditsMenu()
    {
        creditsMenu.SetActive(true);
        mainMenu.SetActive(false);
        controlsMenu.SetActive(false);
    }

    public void quitGame()
    {
        Application.Quit();
        Debug.Log("Quitting Game");
    }
}
