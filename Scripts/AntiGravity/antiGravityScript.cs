using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class antiGravityScript : MonoBehaviour
{
    public bool isOn = false;
    public GameObject indication;
    public GameObject player;
   
    public List<GameObject> enemies = new List<GameObject>();

    private void Update()
    {
        if(player.transform.position.y <= -30f && enemies.Count <= 0)
        {
            indication.SetActive(true);
        }

        for (int i = 0; i < enemies.Count;)
        {
            if (enemies[i] == null)
            {
                enemies.Remove(enemies[i]);
                continue;
            }
            i++;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(isOn && enemies.Count <= 0)
        {
            collision.attachedRigidbody.velocity = new Vector2(collision.attachedRigidbody.velocity.x, 2f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && collision.transform.position.y <= -30f)
        {
            isOn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            isOn = false;
            indication.SetActive(false);
        }
    }
}
