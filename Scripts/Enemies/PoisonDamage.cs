using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonDamage : MonoBehaviour
{
    private bool poisonImmunity = false;
    private PlayerController playerController;

    private void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (poisonImmunity == false)
            {
                if (playerController.oxygenCounter.Oxygen <= 0) //if player is out of 02, kill them
                {
                    //Play Death Animation
                    playerController.animationScript.playDead = true;
                    return;
                }
                else
                {
                    StartCoroutine(poisonCollision());
                    playerController.oxygenCounter.Oxygen -= 2; //make them lose 10 O2
                    playerController.collisionScript.SprayOxygen(2); //calls the spray function spraying all oxygen

                }
            }
        }
    }
    private IEnumerator poisonCollision()
    {
        poisonImmunity = true;
        yield return new WaitForSeconds(0.5f);
        poisonImmunity = false;
    }
}
