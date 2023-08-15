using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gateManager : MonoBehaviour
{
    public bool openTrigger = false;
    public List<GameObject> enemies;
    private Vector2 originalPosition;
    [SerializeField]
    private float openingDistance = 100f;
    public bool horizontalOpen;
    public bool useEnemiesToOpen = false;

    //Audio
    public AudioSource audiosource;
    public AudioClip openClip;

    private void Start()
    {
        originalPosition = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i< enemies.Count;)
        {
            if(enemies[i] == null)
            {
                enemies.Remove(enemies[i]);
                continue;
            }
            i++;
        }

        if (openTrigger || (enemies.Count <= 0 && useEnemiesToOpen))
        {
            if (!horizontalOpen)
            {
                if (transform.position.y <= originalPosition.y + openingDistance)
                {
                    transform.position = new Vector2(transform.position.x, transform.position.y + 0.01f);
                }
                else
                {
                    //Play Open Audio Clip
                    audiosource.PlayOneShot(openClip);
                    //Reset Boolean
                    openTrigger = false;
                    useEnemiesToOpen = false;
                    enemies.Add(gameObject);
                }
            }
            else if(horizontalOpen)
            {
                if (transform.position.x <= originalPosition.x + openingDistance)
                {
                    transform.position = new Vector2(transform.position.x + 0.01f, transform.position.y);
                }
                else
                {
                    //Play Open Audio Clip
                    audiosource.PlayOneShot(openClip);
                    //Reset Boolean
                    openTrigger = false;
                    useEnemiesToOpen = false;
                    enemies.Add(gameObject);
                }

            }
        }
    }
}
