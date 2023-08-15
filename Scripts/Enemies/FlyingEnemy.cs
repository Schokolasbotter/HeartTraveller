using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    // private rigid
    [SerializeField]
    private float vertHeight;
    [SerializeField]
    private float vertSpeed;
    [SerializeField]
    private float horizontalSpeed;

    private float startY;
    private int yDir = 1;

    [SerializeField]
    private float leftXPatrol;
    [SerializeField]
    private float rightXPatrol;
    private int xDir = 1;
    // Start is called before the first frame update
    void Start()
    {
        startY = transform.position.y; //start y = current transform
       
    }

    // Update is called once per frame
    void Update()
    {
        
        float yComp; //new var - yComponent for vector
        if(transform.position.y >= startY + vertHeight) //if its too high
        {
            yDir = 0; //y direction =0
        }
        if (transform.position.y <= startY - vertHeight) //if its too low
        {
            yDir = 1; // y direction = 1 
        }
        if (yDir == 1)
        {
            yComp = vertSpeed;
        } else
        {
            yComp = -vertSpeed;
        }

        if (transform.position.x <= leftXPatrol) //if too far left
        {
            xDir = 0; //xDirection = 0
        }
        if (transform.position.x >= rightXPatrol) //if too far right
        {
            xDir = 1; //xDirection = 1
        }
        if (xDir == 0)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(horizontalSpeed, yComp); //add speed to the right and the y comp
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-horizontalSpeed, yComp);//add speed to the left and the y comp
        }
     
    }
}
