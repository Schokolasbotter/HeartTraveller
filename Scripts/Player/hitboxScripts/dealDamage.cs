using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dealDamage : MonoBehaviour
{
    public int damageLvl1 = 10;
    public int damageLvl2 = 20;
    public int damageLvl3 = 30;

    internal PlayerController playerController;
    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>(); //finds the player controller script and attaches it to the variable
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy") //checks if the enemy hit is an enemy before attempting to deal damage to it
        {
            //Deal Damage according to talents
            if(playerController.talents.Offensive >= 3)
            {
                collision.gameObject.GetComponent<HealthManagement>().takeDamage(damageLvl3); //deals damage equal to the damage variable
            }
            else if(playerController.talents.Offensive >= 1)
            {
                collision.gameObject.GetComponent<HealthManagement>().takeDamage(damageLvl2); //deals damage equal to the damage variable
            }
            else if (playerController.talents.Offensive < 1)
            {
                collision.gameObject.GetComponent<HealthManagement>().takeDamage(damageLvl1); //deals damage equal to the damage variable
            }

            if (playerController.abilityScript.isStomping == true) //if the player is in a stomp attack
            {
                Debug.Log("Stomping Collision");
                playerController.abilityScript.isStomping = false;  //changes is stomping to true
                playerController.abilityScript.StompContactJump();  //calls a contact jump function that adds a small jump after contact is made
            }



            Vector2 directionVector = collision.gameObject.transform.position - gameObject.transform.position; //creates a vector from player to target
            directionVector.y = 0;  //sets y component to 0
            directionVector.Normalize(); //normalize
            directionVector = Vector2.Scale(directionVector, new Vector2(5f,1f)); //scale it by 5
            if(collision.gameObject.GetComponent<KnockbackManagement>() != null)
            {
                collision.gameObject.GetComponent<KnockbackManagement>().knockback(directionVector); //call knockback function to the collision
            }            
        }
        else
        {
            Debug.Log("Not an enemy");
        }
    }
}
