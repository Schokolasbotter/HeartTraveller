using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//------------------------------------------------------------------------
// Player Movement Script
//------------------------------------------------------------------------
public class PlayerMovementScript : MonoBehaviour
{
    // Player Controller Reference
    [SerializeField]
    internal PlayerController playerController;

    //PlayerMovementScript Variables
    private Rigidbody2D rb;
    internal bool isGrounded;
    private bool isWalled;
    private bool canDoubleJump = false;
    internal bool canBeControlled = true;

    //Variables for movement
    [SerializeField]
    private float runSpeed = 1f;
    [SerializeField]
    private float jumpSpeed = 7f;
    [SerializeField]
    private float floatSpeed = 4f;
    [SerializeField]
    private float climbingSpeed = 3f;

    //Platform Movement
    [SerializeField]
    internal PlatformEffector2D effector;
    [SerializeField]
    private float platformTimer = 0.25f;

    //Heart
    [SerializeField]
    internal GameObject heartObject;
    [SerializeField]
    internal Vector2 heartPosition;
    public bool endGame = false;

    private void Start()
    {
        //Get the rigid body
        rb = playerController.rb2d;
        checkTalents();
    }

    void FixedUpdate()
    {
        //Check if touching the Ground
        groundedCheck();

        //If talented check if touching the wall
        if(playerController.talents.Mobility > 3)
        {
            walledCheck();
        }        

        //Basic Movement
        if (!playerController.abilityScript.isDashing)
        {
            //Horizontal Movement
            horizontalPlayerMovement();
            //Vertical Movement Different for Wallclimbing then for regular
            if(isWalled)
            {
                wallPlayerMovement();
            }
            else
            {
                verticalPlayerMovement();
            }            
        }

        //End Game
        if(endGame)
        {
            endOfGame();
        }
    }
    void horizontalPlayerMovement()
    {
        if (canBeControlled == true)
        {
            if (playerController.inputScript.isRight && playerController.abilityScript.isStomping == false) //stops any movement when the stomp attack is happening
            {
                if(isGrounded) // If on the floor
                {
                    //Set Velocity on X-axis
                    rb.velocity = new Vector2(runSpeed, rb.velocity.y);
                }
                else if(!isGrounded)
                {
                    //Set Velocity on X-axis
                    rb.velocity = new Vector2(floatSpeed, rb.velocity.y);
                }
                
            }
            else if (playerController.inputScript.isLeft && playerController.abilityScript.isStomping == false) //stops any movement when the stomp attack is happening
            {
                if (isGrounded) // If on the floor
                {
                    //Set Velocity on X-axis
                    rb.velocity = new Vector2(-runSpeed, rb.velocity.y);
                }
                else if (!isGrounded)
                {
                    //Set Velocity on X-axis
                    rb.velocity = new Vector2(-floatSpeed, rb.velocity.y);
                }
            }
            else
            {
                //Set Velocity on X-axis to 0 if no Input
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }
    }

    void verticalPlayerMovement()
    {
        //If Up Input
        if (playerController.inputScript.isUp)
        {
            //Reset effector for platforms
            effector.rotationalOffset = 0f;

            if (isGrounded) // Check if Grounded to Jump
            {
                //Play Jump Audio Clip
                playerController.audioSource.PlayOneShot(playerController.jumpClip);
                //Reset Input Boolean
                playerController.inputScript.isUp = false;
                //Play Jump Animation
                playerController.animationScript.playJump = true;
                //Set Velocity to Jump without changing X Velocity
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            }
            //if in the air and DJ is available then jump
            else if (canDoubleJump)
            {
                //Play Jump Audio Clip
                playerController.audioSource.PlayOneShot(playerController.jumpClip);
                //Reset Input Boolean
                playerController.inputScript.isUp = false;
                //Change DJ Boolean
                canDoubleJump = false;
                //Play Jump Animation
                playerController.animationScript.playJump = true;
                //Set Velocity to Jump without changing X Velocity
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            }
            else
            {
                //Reset Input Boolean
                playerController.inputScript.isUp = false;
            }
        }
        if(playerController.inputScript.isDown)
        {
            if(platformTimer <= 0)
            {
                effector.rotationalOffset = 180f;
                platformTimer = 0.25f;
                StartCoroutine(revertOffset());
            }
            else
            {
                platformTimer -= Time.deltaTime;
            }
        }
        else if(!playerController.inputScript.isDown)
        {
            platformTimer = 0.25f;
            
        }
    }

    void wallPlayerMovement()
    {
        //when Up Input
        if(playerController.inputScript.isUp)
        {
            //Set gravityscale
            rb.gravityScale = 1;
            //Change Velocity
            rb.velocity = new Vector2(rb.velocity.x, climbingSpeed);
            //Set Animation Boolean
            playerController.animationScript.animator.SetBool("isWallClimbing", true);
        }
        //when isDown
        else if(playerController.inputScript.isDown)
        {
            //Set gravityscale
            rb.gravityScale = 1;
            //Change Velocity
            rb.velocity = new Vector2(rb.velocity.x, -climbingSpeed);
            //Set Animation Boolean
            playerController.animationScript.animator.SetBool("isWallClimbing", true);
        }
        //when none
        else
        {
            //Set gravityscale to slide down the wall
            rb.gravityScale = 0.7f;
            //Keep X Axis Velocity available to jump of wall
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            //Animation
            playerController.animationScript.animator.SetBool("isWallClimbing", false);
        }
    }

    void groundedCheck()
    {
        //Create Layer Mask
        int ground = 1 << LayerMask.NameToLayer("Ground");
        int platform = 1 << LayerMask.NameToLayer("Platform");
        int mask = ground | platform;
        //IF Ground Collider is in the Linecast
        if (Physics2D.Linecast(transform.position, playerController.groundcheckL.position, mask)
            || Physics2D.Linecast(transform.position, playerController.groundcheckR.position, mask)
            || Physics2D.Linecast(transform.position, playerController.groundcheckR2.position, mask)
            || Physics2D.Linecast(transform.position, playerController.groundcheckL2.position, mask))
        {
            //Set Boolean
            isGrounded = true;

            //If talented set DJ to available
            if (playerController.talents.Mobility > 2)
            {
                canDoubleJump = true;
            }
        }
        else 
        {
            //Otherwise is not grounded
            isGrounded = false;
        }
    }

    private void walledCheck()
    {
        //Check if Wall Collider is in Linecast
        if (Physics2D.Linecast(transform.position, playerController.wallcheckL.position, 1 << LayerMask.NameToLayer("Wall"))
            || Physics2D.Linecast(transform.position, playerController.wallcheckL2.position, 1 << LayerMask.NameToLayer("Wall"))
            || Physics2D.Linecast(transform.position, playerController.wallcheckR.position, 1 << LayerMask.NameToLayer("Wall"))
            || Physics2D.Linecast(transform.position, playerController.wallcheckR2.position, 1 << LayerMask.NameToLayer("Wall")))
        {
            //If not already walled
            if(!isWalled)
            {                
                //send Trigger to start wall animations only on first connection to wall
                playerController.animationScript.animator.SetTrigger("isWalled");
            }
            //Set Boolean
            isWalled = true;

            //Reset Double Jump
            canDoubleJump = true;
            //Start Wall Animations
            playerController.animationScript.playWalled = true;
        }
        else
        {
            //Reset Boolean
            isWalled = false;
            //Reset Animation to start exit animation
            playerController.animationScript.playWalled = false;
            //Reset Gravity Scale
            rb.gravityScale = 1f;
        }
    }

    public void checkTalents()
    {
        if (playerController.talents.Mobility > 0)
        {
            runSpeed = runSpeed* 2;
            floatSpeed = floatSpeed* 1.5f;
        }

        if (playerController.talents.Mobility > 2)
        {
           canDoubleJump = true;
        }
    }

    IEnumerator revertOffset()
    {
        //wait
        yield return new WaitForSeconds(0.5f);
        //Reset effector offset
        effector.rotationalOffset = 0f;
    }

    public void endOfGame()
    {
        if(heartObject != null)
        {
            //Set gravity to 0;
            rb.gravityScale = 0;
            //Move towards heart position
            transform.position = Vector2.Lerp(transform.position, heartPosition,Time.deltaTime);
            if(transform.localScale.x > 0)
            {
                transform.localScale = new Vector3(transform.localScale.x - 0.001f, transform.localScale.y - 0.001f, 0f);
            }            
            else
            {
                heartObject.GetComponent<HeartScript>().endOfGame = true;
            }
        }
    }
}
