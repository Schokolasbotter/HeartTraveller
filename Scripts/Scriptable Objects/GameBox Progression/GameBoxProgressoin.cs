using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameBoxProgressoin : MonoBehaviour
{
    private bool boxEnabled = true;
    [SerializeField]
    private GameObject controllableText;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(boxEnabled == true && collision.tag == "Player")
        {
            StartCoroutine(Fader());
        }
    }

    private IEnumerator Fader() 
    {
        Color tmp = controllableText.GetComponent<Text>().color;
        //Debug.Log(tmp);
        while (tmp.a < 1) //while the alpha isn't 0%
        {
            yield return new WaitForSeconds(0.05f); //wait 0.05 seconds
            tmp.a += 0.1f; //alpha -= 1%
            controllableText.GetComponent<Text>().color = tmp; //sets alpha of the oxygen to the new value
        }
        Destroy(gameObject);
    }
}
