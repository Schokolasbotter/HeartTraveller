using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oxygenScript : MonoBehaviour
{
    [SerializeField]
    private Animator oxygenAnimator;
    public int value = 1;
    private new Rigidbody2D rigidbody2D;
    [SerializeField] 
    internal bool colliderActive = false;

    //Audio
    public AudioSource audiosource;
    public AudioClip bubbleClip;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(preventCollision());
        rigidbody2D = GetComponent<Rigidbody2D>();

        //Set Value for Type of Animation
        oxygenAnimator.SetInteger("value", value);        
    }

    private void OnTriggerEnter2D(Collider2D collision) //when the box collider detects a collision
    {
        if (colliderActive == true)
        {
            if (collision.gameObject.tag == "Player") //if its with a player
            {
                StartCoroutine(collect());
            }
            else if (collision.gameObject.tag == "Ground") //if its with the ground
            {
                rigidbody2D.gravityScale = 0; //stop gravity
                rigidbody2D.velocity = new Vector2(0, 0); //stops motion
                rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll; //freeze the position
            }
        }
    }

    private IEnumerator preventCollision() //function that prevents collisions from being detected for the first 0.3 seconds of the oxygens life
    {
        yield return new WaitForSeconds(1f); //waits for 3 seconds
        colliderActive = true; //sets the collider to being active
    }

    private IEnumerator collect()
    {
        //Play Bubble audio
        audiosource.PlayOneShot(bubbleClip);
        //Deactivate Collider
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        //Wait
        yield return new WaitForSeconds(bubbleClip.length);
        //Destroy Object
        Destroy(gameObject); //destroy it
    }

    
}

