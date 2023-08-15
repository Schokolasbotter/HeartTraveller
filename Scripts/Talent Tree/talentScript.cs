using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class talentScript : MonoBehaviour
{
    //Talent Menu Variables
    [SerializeField]
    private ScriptableObject talents;
    [SerializeField]
    private Text oxygenText;

    public PlayerController player;
    public bool isCheckpoint = false;
    // Button Arrays
    [SerializeField]
    private Button[] offense;

    [SerializeField]
    private Button[] defense;

    [SerializeField]
    private Button[] mobility;

    //Costs
    [SerializeField]
    private int tier1Cost;
    [SerializeField]
    private int tier2Cost;
    [SerializeField]
    private int tier3Cost;
    [SerializeField]
    private int tier4Cost;

    // Start is called before the first frame update
    void Start()
    {
        //Get the PlayerController Script
        player = GameObject.Find("MainCharacter").GetComponent<PlayerController>();             
    }

    private void Update()
    {
        //Update Oxygen Text on Talent Menu
        oxygenText.text = player.oxygenCounter.Oxygen.ToString();
        //Set Buttons
        setButtonsInteractable();
    }

    public void offensiveTalentInc()
    {
        if(isCheckpoint)
        {
            //Reduce Oxygen Counter value by the correct cost
            if (player.talents.Offensive == 0 && player.oxygenCounter.Oxygen >= tier1Cost)
            {
                //Reduce Oxygen Count
                player.oxygenCounter.Oxygen -= tier1Cost;
                //Increase Talent Scriptable Object
                player.talents.Offensive += 1;
            }
            else if (player.talents.Offensive == 1 && player.oxygenCounter.Oxygen >= tier2Cost)
            {
                //Reduce Oxygen Count
                player.oxygenCounter.Oxygen -= tier2Cost;
                //Increase Talent Scriptable Object
                player.talents.Offensive += 1;
            }
            else if (player.talents.Offensive == 2 && player.oxygenCounter.Oxygen >= tier3Cost)
            {
                //Reduce Oxygen Count
                player.oxygenCounter.Oxygen -= tier3Cost;
                //Increase Talent Scriptable Object
                player.talents.Offensive += 1;
            }
            else if (player.talents.Offensive == 3 && player.oxygenCounter.Oxygen >= tier4Cost)
            {
                //Reduce Oxygen Count
                player.oxygenCounter.Oxygen -= tier4Cost;
                //Increase Talent Scriptable Object
                player.talents.Offensive += 1;
            }
        }        
    }

    public void defensiveTalentInc()
    {
        if(isCheckpoint)
        {
            //Reduce Oxygen Counter value by the correct cost
            if (player.talents.Defensive == 0 && player.oxygenCounter.Oxygen >= tier1Cost)
            {
                //Reduce Oxygen Count
                player.oxygenCounter.Oxygen -= tier1Cost;
                //Increase Talent Scriptable Object
                player.talents.Defensive += 1;
            }
            else if (player.talents.Defensive == 1 && player.oxygenCounter.Oxygen >= tier2Cost)
            {
                //Reduce Oxygen Count
                player.oxygenCounter.Oxygen -= tier2Cost;
                //Activate Shield
                player.abilityScript.shieldEnabled = true;
                player.animationScript.Shield = true;
                //Increase Talent Scriptable Object
                player.talents.Defensive += 1;
            }
            else if (player.talents.Defensive == 2 && player.oxygenCounter.Oxygen >= tier3Cost)
            {
                //Reduce Oxygen Count
                player.oxygenCounter.Oxygen -= tier3Cost;
                //Increase Talent Scriptable Object
                player.talents.Defensive += 1;
            }
            else if (player.talents.Defensive == 3 && player.oxygenCounter.Oxygen >= tier4Cost)
            {
                //Reduce Oxygen Count
                player.oxygenCounter.Oxygen -= tier4Cost;
                //Increase Talent Scriptable Object
                player.talents.Defensive += 1;
                //Change Shield Cooldown
                player.GetComponent<PlayerController>().abilityScript.shieldCooldown = 30f;
            }
        }        
    }

    public void mobilityTalentInc()
    {
        if(isCheckpoint)
        {
            //Reduce Oxygen Counter value by the correct cost
            if (player.talents.Mobility == 0 && player.oxygenCounter.Oxygen >= tier1Cost)
            {
                //Reduce Oxygen Count
                player.oxygenCounter.Oxygen -= tier1Cost;
                //Increase Talent Scriptable Object
                player.talents.Mobility += 1;
                player.movementScript.checkTalents();
            }
            else if (player.talents.Mobility == 1 && player.oxygenCounter.Oxygen >= tier2Cost)
            {
                //Reduce Oxygen Count
                player.oxygenCounter.Oxygen -= tier2Cost;
                //Increase Talent Scriptable Object
                player.talents.Mobility += 1;
            }
            else if (player.talents.Mobility == 2 && player.oxygenCounter.Oxygen >= tier3Cost)
            {
                //Reduce Oxygen Count
                player.oxygenCounter.Oxygen -= tier3Cost;
                //Increase Talent Scriptable Object
                player.talents.Mobility += 1;
            }
            else if (player.talents.Mobility == 3 && player.oxygenCounter.Oxygen >= tier4Cost)
            {
                //Reduce Oxygen Count
                player.oxygenCounter.Oxygen -= tier4Cost;
                //Increase Talent Scriptable Object
                player.talents.Mobility += 1;
            }
        }        
    }    

    private void setButtonsInteractable()
    {
        //Offense
        //Loop through the array
        for (int i = 0; i < 4; i++)
        {
            if (i < player.talents.Offensive) // If already past the tier 
            {
                offense[i].interactable = false; //Make Button non interactable
                offense[i].image.color = Color.red; //Change Color to red
            }
            else if (i == player.talents.Offensive) // If talent is skillable 
            {
                offense[i].interactable = true; // Make Button interactable
                offense[i].image.color = Color.white; //Change Color to white
            }
            else if (i > player.talents.Offensive) //If talent not skillable yet
            {
                offense[i].interactable = false; // Make button non interactable
                offense[i].image.color = Color.gray; // Change color to gray
            }
        }

        //Defense
        for (int i = 0; i < 4; i++)
        {
            if (i < player.talents.Defensive) // If already past the tier 
            {
                defense[i].interactable = false; //Make Button non interactable
                defense[i].image.color = Color.red; //Change Color to red
            }
            else if (i == player.talents.Defensive) // If talent is skillable 
            {
                defense[i].interactable = true; // Make Button interactable
                defense[i].image.color = Color.white; //Change Color to white
            }
            else if (i > player.talents.Defensive) //If talent not skillable yet
            {
                defense[i].interactable = false;
                defense[i].image.color = Color.gray;
            }
        }

        //Mobility
        for (int i = 0; i < 4; i++)
        {
            if (i < player.talents.Mobility) // If already past the tier 
            {
                mobility[i].interactable = false; //Make Button non interactable
                mobility[i].image.color = Color.red; //Change Color to red
            }
            else if (i == player.talents.Mobility) // If talent is skillable 
            {
                mobility[i].interactable = true; // Make Button interactable
                mobility[i].image.color = Color.white; //Change Color to white
            }
            else if (i > player.talents.Mobility) //If talent not skillable yet
            {
                mobility[i].interactable = false; // Make button non interactable
                mobility[i].image.color = Color.gray; // Change color to gray
            }
        }
    }
}
