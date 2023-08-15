using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointScript : MonoBehaviour
{
    [SerializeField]
    private talentScript talentScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<PlayerController>().lastCheckpoint = gameObject.transform.position;
            collision.GetComponent<PlayerController>().checkpointOxygen = collision.GetComponent<PlayerController>().oxygenCounter.Oxygen;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            talentScript.isCheckpoint = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            talentScript.isCheckpoint = false;
        }
    }
}
