using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIScript : MonoBehaviour
{
    //Player Controller Reference
    [SerializeField]
    internal PlayerController playerController;

    //Ui References
    [SerializeField]
    internal Text OxygenText;

    [SerializeField]
    internal Image DashImage;

    [SerializeField]
    internal Image DashCooldownImage;

    [SerializeField]
    internal Image shieldCooldownImage;

    //Dash Cooldown Variables
    public bool dashCooldown = false;
    private float dashTimer = 0f;

    //Shield Cooldown Variables
    public bool shieldCooldown = false;
    private float shieldTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        //Set the correct number at start
        OxygenText.text = playerController.oxygenCounter.Oxygen.ToString();

        //Set Dash Cooldown Fill Amount
        DashCooldownImage.fillAmount = 0f;

        //Set Shield Cooldown Fill Amount
        shieldCooldownImage.fillAmount = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //Update number of Oxygen collected
        OxygenText.text = playerController.oxygenCounter.Oxygen.ToString();


        //Dash Cooldown
        manageDashCooldown();
        //Shield Cooldown
        manageShieldCooldown();
    }

    private void manageDashCooldown()
    {
        if (dashCooldown)
        {
            //Set Fill Amount to 1;
            DashCooldownImage.fillAmount = 1f;
            //Set Timer
            dashTimer = playerController.abilityScript.dashCooldown;
            //Reset Boolean
            dashCooldown = false;
        }

        //Decrease Timer
        dashTimer -= Time.deltaTime;
        // Set Fill Amount
        DashCooldownImage.fillAmount = dashTimer / playerController.abilityScript.dashCooldown;
    }

    private void manageShieldCooldown()
    {
        if (shieldCooldown)
        {
            //Set Fill Amount to 1;
            shieldCooldownImage.fillAmount = 1f;
            //Set Timer
            shieldTimer = playerController.abilityScript.shieldCooldown;
            //Reset Boolean
            shieldCooldown = false;
        }

        //Decrease Timer
        shieldTimer -= Time.deltaTime;
        // Set Fill Amount
        shieldCooldownImage.fillAmount = shieldTimer / playerController.abilityScript.shieldCooldown;
    }
}
