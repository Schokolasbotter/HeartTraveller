using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//------------------------------------------------------------------------
// Player Collision Script
//------------------------------------------------------------------------
public class PlayerCollisionScript : MonoBehaviour
{
    // Player Controller Reference
    [SerializeField]
    internal PlayerController playerController;
    internal bool iFrames = false;
    [SerializeField]
    private GameObject o2;

    //Collision Script Variables

    //Collision detection, on collision enter with another hitbox, if its an enemy debuglog a hit unless a shield is up
    void OnCollisionEnter2D(Collision2D other) //when a collision happens with the player
    {
        if (other.gameObject.tag == "Enemy" && iFrames == false) //if its with an enemy, and there are no invinsibility frames
        {
            //Debug.Log("Enemy!!!!!!");
            //if the shield is enabled
            if (playerController.abilityScript.shieldEnabled == true)
            {
                //Play Shield Pop Audio Clip
                playerController.audioSource.PlayOneShot(playerController.shieldPopClip);
                //Debug.Log("Shield");
                playerController.abilityScript.startShieldCD(); //ignores the damage and resets shield cooldown
                //Destroy Shield Animation
                playerController.animationScript.Shield = false;
            } 
            else
            {
                if (playerController.oxygenCounter.Oxygen <= 0) //if player is out of 02, kill them
                {
                    //Play Death Aduio Clip
                    playerController.audioSource.PlayOneShot(playerController.deathClip);
                    //Play Death Animation
                    playerController.animationScript.playDead = true;
                    return;
                }
                else if (playerController.talents.Defensive < 2) //if the player has low defense
                {
                    //Play Hit Audio Clip
                    playerController.audioSource.PlayOneShot(playerController.hitClip);
                    SprayOxygen(playerController.oxygenCounter.Oxygen); //calls the spray function spraying all oxygen
                    playerController.oxygenCounter.Oxygen -= playerController.oxygenCounter.Oxygen; //make them lose all O2 
                    playerController.animationScript.playHit = true; //play the hit animation
                }
                else if (playerController.talents.Defensive >= 2)
                {
                    //Play Hit Audio Clip
                    playerController.audioSource.PlayOneShot(playerController.hitClip);

                    if (playerController.oxygenCounter.Oxygen < 10)
                    {
                        SprayOxygen(playerController.oxygenCounter.Oxygen);
                        playerController.oxygenCounter.Oxygen = 0;
                        playerController.animationScript.playHit = true; //play the hit animation
                    }
                    else
                    {
                        SprayOxygen(playerController.oxygenCounter.Oxygen / 2);
                        playerController.oxygenCounter.Oxygen -= playerController.oxygenCounter.Oxygen / 2;
                        playerController.animationScript.playHit = true; //play the hit animation
                    }
                }         
            }


            if (playerController.talents.Defensive >= 3)
            {
                other.gameObject.GetComponent<HealthManagement>().takeDamage(5);
            }
            StartCoroutine(CreateIFrames(0.5f));

            Vector2 directionVector = other.gameObject.transform.position - transform.position;
            playerController.rb2d.velocity = new Vector2(directionVector.x * -1, 5);
            if (other.gameObject.GetComponent<KnockbackManagement>() != null)
            {
                other.gameObject.GetComponent<KnockbackManagement>().knockback(directionVector);
            }
        }
        else if (other.gameObject.tag == "Ground")
        {
            //Play Landing Animation
            playerController.animationScript.playLanding = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        //Collect Oxygen with the correct Value
        if (trigger.gameObject.tag == "Oxygen" && trigger.GetComponent<oxygenScript>().colliderActive == true)
        {
            playerController.oxygenCounter.Oxygen += trigger.gameObject.GetComponent<oxygenScript>().value;
        }

    }

    internal IEnumerator CreateIFrames(float iFrameNumber) //a function that creates invisibility frames for the player
    {
        //Debug.Log("IFRAME");
        playerController.movementScript.canBeControlled = false; //stops player from being able to control the player, as this is done for them
        iFrames = true; //sets iframes to true
        yield return new WaitForSeconds(iFrameNumber); //waits for the iframe time to be over
        iFrames = false; //sets iframes to false
        playerController.movementScript.canBeControlled = true; //allows player to be controlled again;
    }

    internal void SprayOxygen(int quantity) //an oxygen spray function that takes in a number of oxygen to spray
    {
        //Debug.Log(quantity);
        float cosIntervalNum = (float)0.5 / quantity; //lets a variable = to 0.5 * quantity
        cosIntervalNum = cosIntervalNum * (180 / Mathf.PI); //converts to degrees
        for (int i = 0; i < quantity; i++) //for the number of oxygen to spray
        {
            //Debug.Log(i);
            GameObject obj = (GameObject)Instantiate(o2, this.gameObject.transform.position, this.gameObject.transform.rotation); //instantiate an oxygen at current position
            obj.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(cosIntervalNum * i) * 4, 10); //add velocity to the object in an upwards arc, based on a cos function
        }
    }
}
