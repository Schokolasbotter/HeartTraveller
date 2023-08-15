using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public Sprite notPressed;
    public Sprite Pressed;

    public GameObject Gate;

    //Audio
    public AudioSource audiosource;
    public AudioClip buttonClick;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = notPressed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" || collision.tag == "hitbox")
        {
            if(gameObject.GetComponent<SpriteRenderer>().sprite == notPressed)
            {
                //Play Button Click Audio Clip
                audiosource.PlayOneShot(buttonClick);
                //Change Sprite
                gameObject.GetComponent<SpriteRenderer>().sprite = Pressed;
                //Open Gate
                Gate.GetComponent<gateManager>().openTrigger = true;
            }
        }
    }
}
