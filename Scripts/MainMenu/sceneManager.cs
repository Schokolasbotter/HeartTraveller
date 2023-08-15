using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour
{
    [SerializeField]
    private PlayerController playercontroller;
    public void startTutorial()
    {
        //Set Scriptable Objects to starting Value
        playercontroller.oxygenCounter.Oxygen = 0;
        playercontroller.talents.Offensive = 0;
        playercontroller.talents.Defensive = 0;
        playercontroller.talents.Mobility = 0;
        //Load Tutorial
        SceneManager.LoadScene("Tutorial");
    }

    public void loadNextScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "MainMenu")
        {
            //Set Scriptable Objects to starting Value if loading first Level
            playercontroller.oxygenCounter.Oxygen = 0;
            playercontroller.talents.Offensive = 0;
            playercontroller.talents.Defensive = 0;
            playercontroller.talents.Mobility = 0;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void goToMainMenu()
    {
        //Unpause the game
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
