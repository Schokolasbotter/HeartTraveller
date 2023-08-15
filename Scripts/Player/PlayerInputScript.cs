using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//------------------------------------------------------------------------
// Player Input Script
//------------------------------------------------------------------------
public class PlayerInputScript : MonoBehaviour
{
    //Player Controller Reference
    [SerializeField]
    internal PlayerController playerController;

    //Input Script Variables

    internal bool isRight;
    internal bool isLeft;
    internal bool isUp;
    internal bool isDown;
    internal bool isAttacking;
    internal bool isDashing;
    internal bool isStomp;
  
    void Update()
    {
        //----------------------------------------
        // Movement
        //----------------------------------------
        // A Key
        if(Input.GetKey("a"))
        {
            isLeft = true;
        }
        else
        {
            isLeft = false;
        }
        // D Key
        if (Input.GetKey("d"))
        {
            isRight = true;
        }
        else
        {
            isRight = false;
        }
        // W Key
        if (Input.GetKeyDown("w"))
        {
            isUp = true;
        }
        else if(Input.GetKeyUp("w"))
        {
            isUp = false;
        }
        // S Key
        if (Input.GetKeyDown("s"))
        {
            isDown = true;
        }
        else if (Input.GetKeyUp("s"))
        {
            isDown = false;
        }
        //----------------------------------------
        // Attacking
        //----------------------------------------
        if (Input.GetKeyDown("i"))
        {
            isAttacking = true;
        }
        //----------------------------------------
        // Dash
        //----------------------------------------
        if(Input.GetKeyDown("o"))
        {
            isDashing = true;
        }       
        if (Input.GetKeyDown("p")) // p is the hotkey for a stomp attack currently
        {
            isStomp = true; //attempts to start a stomp attack
        }
    }
}
