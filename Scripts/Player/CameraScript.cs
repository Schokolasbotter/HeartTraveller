using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    [SerializeField]
    private float timeOffset = 5f;

    [SerializeField]
    private Vector2 posOffsetL = new Vector2(-2f,0f);

    [SerializeField]
    private Vector2 posOffsetR = new Vector2(2f, 0f);

    [SerializeField]
    float leftLimit;
    [SerializeField]
    float rightLimit;
    [SerializeField]
    float bottomLimit;
    [SerializeField]
    float topLimit;

    private bool LookingLeft = false;
    private bool LookingRight = true;    

    // Update is called once per frame
    void Update()
    {
        if (player != null) // if Player has not died
        {
            lookDirection(); //Change direction Player is looking at

            //Camera Current Position
            Vector3 startPos = transform.position;

            //Player Current Position
            Vector3 endPos = player.position;

            //fix position Offset

            //Change offset depending on the direction Player is looking at
            if (LookingLeft)
            {
                endPos.x += posOffsetL.x;
                endPos.y += posOffsetL.y;
            }
            else if (LookingRight)
            {
                endPos.x += posOffsetR.x;
                endPos.y += posOffsetR.y;
            }

            endPos.z = -10;


            //Change position towards player
            transform.position = Vector3.Lerp(startPos, endPos, timeOffset * Time.deltaTime);

            //Clamp to confine our value to a particular range
            transform.position = new Vector3
                (Mathf.Clamp(transform.position.x, leftLimit, rightLimit),
                Mathf.Clamp(transform.position.y, bottomLimit, topLimit),
                transform.position.z);
        }
    }

    private void lookDirection()
    {
        //Look Left 
        if(Input.GetKey("a"))
        {
            LookingLeft = true;
            LookingRight = false;
        }
        //Look Right
        else if (Input.GetKey("d"))
        {
            LookingLeft = false;
            LookingRight = true;
        }
    }

    void OnDrawGizmos()
    {
        //draw a box around our camera boundary
        Gizmos.color = Color.red;
        //top boundary line
        Gizmos.DrawLine(new Vector2(leftLimit, topLimit), new Vector2(rightLimit, topLimit));
        // right boundary line
        Gizmos.DrawLine(new Vector2(rightLimit, topLimit), new Vector2(rightLimit, bottomLimit));
        // bottom boundary line
        Gizmos.DrawLine(new Vector2(rightLimit, bottomLimit), new Vector2(leftLimit, bottomLimit));
        // left boundary line
        Gizmos.DrawLine(new Vector2(leftLimit, bottomLimit), new Vector2(leftLimit, topLimit));
    }
}
