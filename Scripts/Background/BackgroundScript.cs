using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour
{
    public GameObject cam;
    private float startPosition;
    public float parallexEffect;

    void Start()
    {
        startPosition = transform.position.x;
    }    
    void FixedUpdate()
    {
        float dist = (cam.transform.position.x * parallexEffect);
        transform.position = new Vector2 (startPosition + dist,transform.position.y);
    }
}
