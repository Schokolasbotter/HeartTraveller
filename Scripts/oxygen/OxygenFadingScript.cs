using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenFadingScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Fader()); //starts the fade process
    }
    private IEnumerator Fader() //A new corutine for o2 Fading
    {
        Color tmp = this.gameObject.GetComponent<SpriteRenderer>().color; //cretes a new temp variable controlling the colour of the oxygen
        while (tmp.a > 0) //while the alpha isn't 0%
        {
            yield return new WaitForSeconds(0.05f); //wait 0.05 seconds
            tmp.a -= 0.01f; //alpha -= 1%
            this.gameObject.GetComponent<SpriteRenderer>().color = tmp; //sets alpha of the oxygen to the new value
        }
        Destroy(gameObject); //when alpha is 0%, destroy the object
    }
}
