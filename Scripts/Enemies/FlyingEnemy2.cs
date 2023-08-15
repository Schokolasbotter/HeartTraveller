using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy2 : MonoBehaviour
{
    [SerializeField]
    float leftDistance;
    [SerializeField]
    float rightDistance;
    [SerializeField]
    float bottomDistance;
    [SerializeField]
    float topDistance;


    private float leftLimit;
    private float rightLimit;
    private float bottomLimit;
    private float topLimit;


    [SerializeField]
    float Speed;
    private float timer;
    [SerializeField]
    private float changeDirectionAfter = 3f;
    private Vector2 targetVector;
    [SerializeField]
    private Vector2 originalPosition;

    private void Awake()
    {
        //Set Limits
        leftLimit = transform.position.x - leftDistance;
        rightLimit = transform.position.x + rightDistance;
        topLimit = transform.position.y + topDistance;
        bottomLimit = transform.position.y - bottomDistance;
    }
    void Start()
    {     
        //Set Timer
        timer = changeDirectionAfter + Random.Range(-0.3f,0.3f);
        //create new direction
        targetVector = new Vector2(Random.Range(leftLimit, rightLimit), Random.Range(bottomLimit, topLimit));
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 0)
        {
            //Continue to fly in direction
            transform.position = Vector2.Lerp(transform.position, targetVector, Speed * Time.deltaTime);
            //decrement timer
            timer -= Time.deltaTime;
        }
        else if(timer <=0)
        {
            //create new direction
            targetVector = new Vector2(Random.Range(leftLimit, rightLimit), Random.Range(bottomLimit, topLimit));
            //reset timer
            timer = changeDirectionAfter;
        }

        //Clamp to confine our value to a particular range
        transform.position = new Vector3
            (Mathf.Clamp(transform.position.x, leftLimit, rightLimit),
            Mathf.Clamp(transform.position.y, bottomLimit, topLimit),
            transform.position.z);
    }

    void OnDrawGizmos()
    {           
        //draw a box around our camera boundary
        Gizmos.color = Color.blue;
        //top boundary line
        Gizmos.DrawLine(new Vector2(originalPosition.x - leftDistance, originalPosition.y + topDistance), new Vector2(originalPosition.x + rightDistance, originalPosition.y + topDistance));
        // right boundary line
        Gizmos.DrawLine(new Vector2(originalPosition.x + rightDistance, originalPosition.y + topDistance), new Vector2(originalPosition.x + rightDistance, originalPosition.y - bottomDistance));
        // bottom boundary line
        Gizmos.DrawLine(new Vector2(originalPosition.x + rightDistance, originalPosition.y - bottomDistance), new Vector2(originalPosition.x - leftDistance, originalPosition.y - bottomDistance));
        // left boundary line
        Gizmos.DrawLine(new Vector2(originalPosition.x - leftDistance, originalPosition.y - bottomDistance), new Vector2(originalPosition.x - leftDistance, originalPosition.y + topDistance));
    }
}
