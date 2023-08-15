using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeartScript : MonoBehaviour
{
    public GameObject endOfGameScreen;
    public bool endOfGame = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            //Block Inputs
            collision.GetComponent<PlayerInputScript>().enabled = false;
            //Set Velocity to 0;
            collision.attachedRigidbody.velocity = new Vector2(0f, 0f);
            collision.GetComponent<PlayerMovementScript>().endGame = true;
        }        
    }
    void Update()
    {
        if (endOfGame == true)
        {
            StartCoroutine(endTheGame());
        }
    }

    IEnumerator endTheGame()
    {
        //Pause the game
        //Time.timeScale = 0f;
        //Open Finish Level UI
        endOfGameScreen.SetActive(true);
        //Wait
        yield return new WaitForSeconds(3f);
        // Load Main Menu        
        SceneManager.LoadScene("MainMenu");
    }

}
