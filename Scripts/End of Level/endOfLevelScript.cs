using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endOfLevelScript : MonoBehaviour
{
    public float waitingTime = 5f;
    public GameObject endOfLevel;
    public GameObject playUI;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            StartCoroutine(nextLevel());
        }
    }

    IEnumerator nextLevel()
    {
        //Pause the game
        //Time.timeScale = 0f;
        //Open Finish Level UI
        endOfLevel.SetActive(true);
        //Wait
        yield return new WaitForSeconds(waitingTime);
        // Load Next Scene
        //Get Active Scene
        Scene currentScene = SceneManager.GetActiveScene();
        if(currentScene.name == "Tutorial")
        {
            SceneManager.LoadScene("MainMenu");
        }
        else
        {            
            SceneManager.LoadScene(currentScene.buildIndex + 1);           
        }
    }
}
