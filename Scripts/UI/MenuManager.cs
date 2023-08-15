using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject talentMenu;
    public GameObject playUI;
    public GameObject shieldIcon;
    public PlayerController playerController;
    public GameObject tooltipObject;
    public GameObject deathMenu;

    private void Start()
    {
        //Check if shield is talented
        checkShieldTalent();   
    }
    // Update is called once per frame
    void Update()
    {
        //Check if shield is talented
        checkShieldTalent();

        //Keyboard Input Detection
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(!pauseMenu.activeInHierarchy)
            {
                
                openPauseMenu();
            }
            else
            {
                resumeGame();
            }  
        }
    }

    public void openPauseMenu()
    {
        // Deactivate Menu Objects 
        talentMenu.SetActive(false);
        playUI.SetActive(false);
        //Activate Pause Object
        pauseMenu.SetActive(true);
        //Pause the game
        Time.timeScale = 0f;
    }

    public void resumeGame()
    {
        //Deactivate Menu Objects
        pauseMenu.SetActive(false);
        //Activate Play UI
        playUI.SetActive(true);
        //Unpause the game
        Time.timeScale = 1f;
    }

    public void openTalentsMenu()
    {
        //Deactive Menu Objects
        pauseMenu.SetActive(false);
        //Activate Talent Object
        talentMenu.SetActive(true);
    }

    public void backtoPauseMenu()
    {
        //Deactive Talent Object
        talentMenu.SetActive(false);
        //Activate Pause Menu Object
        pauseMenu.SetActive(true);
        //Deactivate Tooltip
        tooltipObject.SetActive(false);
    }

    private void checkShieldTalent()
    {
        if (playerController.talents.Defensive > 1)
        {
            shieldIcon.SetActive(true);
        }
        else
        {
            shieldIcon.SetActive(false);
        }
    }

    public void respawn()
    {
        //Stop the pause
        Time.timeScale = 1f;
        //Position Player on last checkpoint
        playerController.gameObject.transform.position = playerController.lastCheckpoint;
        //Reset Oxygen to last saved Oxygen
        playerController.oxygenCounter.Oxygen = playerController.checkpointOxygen;
        //Activate the Player
        playerController.gameObject.SetActive(true);
        //Deactivate the Death Menu
        deathMenu.SetActive(false);
        playerController.abilityScript.canDash = true;
    }
}
