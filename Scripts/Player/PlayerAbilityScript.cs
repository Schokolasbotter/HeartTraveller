using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//------------------------------------------------------------------------
// Player Ability Script
//------------------------------------------------------------------------
public class PlayerAbilityScript : MonoBehaviour
{
    // Player Controller Reference
    [SerializeField]
    internal PlayerController playerController;

    //Ability Script Variables
    private Rigidbody2D rb;
    private bool lookingLeft = false;
    private bool lookingRight = true;

    //Dash
    [SerializeField]
    private float dashDuration = 0.2f;
    [SerializeField]
    private float dashSpeed = 20f;
    internal bool isDashing = false;
    public bool canDash = true;
    public float dashCooldown = 5f;

    //Attack
    internal bool isAttacking = false;
    [SerializeField]
    private Transform hitboxPosition;
    [SerializeField]
    private GameObject attackHitbox;   
    [SerializeField]
    private GameObject dashHitbox;
    [SerializeField]
    private AnimationClip attackAnimation;
    [SerializeField]
    internal int lvl1Damage = 10;
    [SerializeField]
    internal int lvl3Damage = 20;
    [SerializeField]
    internal float attackRate = 2f;
    [SerializeField]
    private GameObject stompHitbox;
    private float attackTimer;
    internal bool isStomping = false;

    //Shield
    internal bool shieldEnabled = false;
    public float shieldCooldown = 60f;

    private void Start()
    {
        //Get Rigid Body
        rb = playerController.rb2d;

        //Mobility Talents
        if(playerController.talents.Mobility > 1)
        {
            //If talented icrease the speed of the dash, which covers more distance in the same amount of time
            dashSpeed = dashSpeed * 1.5f;
        }
        //Defensive
        //Shield
        if(playerController.talents.Defensive > 1)
        {
            playerController.abilityScript.shieldEnabled = true;
            playerController.animationScript.Shield = true;
        }
        //Shield Cooldown
        if(playerController.talents.Defensive >= 4)
        {
            shieldCooldown = 30f;
        }
    }

    private void Update()
    {
        //This is a timer in console to see with cooldown numbers, just uncomment
        //Debug.Log((int)Time.time);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Adjust Direction the character is "looking" at
        adjustLookingDirection();
        //Adjust Hitbox to either left or right
        adjustHitbox();

        //Attack
        if (playerController.inputScript.isAttacking && Time.time >= attackTimer)
        {
            //Set the timer to next moment attack is available
            attackTimer = Time.time + 1f / attackRate;
            //Start Animation
            playerController.animationScript.playAttack = true;
            //Spawn Hitbox
            StartCoroutine(SpawnHitbox());
            //Reset Input Boolean
            playerController.inputScript.isAttacking = false;
        }
        else
        {
            //Reset Input Boolean
            playerController.inputScript.isAttacking = false;
        }

        //Dash
        if (playerController.inputScript.isDashing && canDash) //If input & the Dash is available
        {
            //Set booleans 
            canDash = false;
            isDashing = true;
            //Start animation
            playerController.animationScript.playDash = true;            
            //Start UI Cooldown
            playerController.UIScript.dashCooldown = true;
            //Reset Input Boolean
            playerController.inputScript.isDashing = false;
            //Start the dash
            StartCoroutine(DashMovement());
        }
        else 
        {
            //Reset Input Boolean if dash is not available
            playerController.inputScript.isDashing = false;
        }

        if(playerController.inputScript.isStomp == true && isStomping == false && playerController.talents.Offensive >= 4) //when the stomp hotkey is pressed, attempts to start a stomp
        {
            Stomp(); //starts stomp
        } else if (isStomping == true && playerController.movementScript.isGrounded == false)
        {
            playerController.rb2d.velocity = new Vector2(0, -10); //adds rapid downward movement locking horizontal movement
        } else if ((isStomping == true && playerController.movementScript.isGrounded == true)) //if it connects, stops the stomp
        {
            isStomping = false; 
            DestroyStompBox();            
        }
    }

    private void adjustLookingDirection()
    {
        if (!isAttacking) // do not adjust direction during attacks
        {
            //Change direction depending on last Input
            if (playerController.inputScript.isLeft)
            {
                lookingLeft = true;
                lookingRight = false;
            }
            else if (playerController.inputScript.isRight)
            {
                lookingRight = true;
                lookingLeft = false;
            }
        }
    }
    private void adjustHitbox() 
    {
        if (!isAttacking) // Do not adjust during attacks
        {
            //Change hitbox Spawner to the correct side depending on last Input
            if (playerController.inputScript.isLeft) 
             {
              hitboxPosition.localPosition = new Vector2(-4f, 0f);
            }
            if (playerController.inputScript.isRight)
            {
               hitboxPosition.localPosition = new Vector2(4f, 0f);
            }
        }
    }

    private IEnumerator SpawnHitbox()
    {
        //Set boolean
        isAttacking = true;
        //Spawn Hitbox        
        Instantiate(attackHitbox, hitboxPosition);  
        //Wait
        yield return new WaitForSeconds(attackAnimation.length);
        //Delete Hitbox
        GameObject hitbox;
        hitbox = GameObject.FindWithTag("hitbox");
        Destroy(hitbox);
        //Set boolean
        isAttacking = false;
    }

    private IEnumerator DashMovement()
    {
        //Play Dash Audio Clip
        playerController.audioSource.PlayOneShot(playerController.dashClip);

        //Spawn Hitbox if talented
        if(playerController.talents.Offensive > 1)
        {
            // Spawn Hitbox
            Instantiate(dashHitbox, this.gameObject.transform);
            //Create IFrames during dash
            StartCoroutine(playerController.collisionScript.CreateIFrames(0.3f));
        }

        //Increase velocity to the right direction depending on the looking direction
        if (lookingRight)
        {
            //Set Velocity
            rb.velocity = (new Vector2(dashSpeed, 0f));
            //Wait
            yield return new WaitForSeconds(dashDuration);
            //Reset boolean
            isDashing = false;
        }
        else if (lookingLeft)
        {
            //Set Velocity
            rb.velocity = (new Vector2(-dashSpeed, 0f));
            //Wait
            yield return new WaitForSeconds(dashDuration);
            //Reset boolean
            isDashing = false;
        }

        //Delete Hitbox
        GameObject hitbox;
        hitbox = GameObject.FindWithTag("hitbox");
        Destroy(hitbox);
        //Reset Input Boolean
        playerController.inputScript.isDashing = false;
        //Start Exit Animation
        playerController.animationScript.playDash = false;
        //Start Cooldown
        StartCoroutine(DashCooldown());
    }
    private IEnumerator DashCooldown()
    {
        //After waiting time reset the boolean to make Dash available again
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
    internal void startShieldCD()
    {
        //Start UI Cooldown
        playerController.UIScript.shieldCooldown = true;
        StartCoroutine(manageShieldCooldown());
    }
    internal IEnumerator manageShieldCooldown()
    {
        shieldEnabled = false;
        yield return new WaitForSeconds(shieldCooldown);
        shieldEnabled = true;
        //Start Shield Animation
        playerController.animationScript.Shield = true;
    }
    private void Stomp() // stomp function
    {
        if(isStomping == false && playerController.movementScript.isGrounded == false) //if player is not currently stomping and it is not grounded aka is mid jump
        {
            isStomping = true; //starts a stomp
            StartCoroutine(playerController.collisionScript.CreateIFrames(0.3f)); //creates some temp iFrames
            Instantiate(stompHitbox, this.gameObject.transform); //creates its hitbox
            Debug.Log("Stompy!");
        } else
        {
            Debug.Log("Cannot Stomp");
        }
        playerController.inputScript.isStomp = false; //resets the isStomp variable
    }
    internal void StompContactJump() //stomp mini jump after contact it an enemy
    {
      //  stompConnect = true; //changes stompConnect allowing a function above to delete the hitbox
        playerController.rb2d.velocity = new Vector2(0, 5); //adds a small amount of jump to the character allowing them to move off of the enemy 
        //and provides some visual and mechanical feedback to the user
        DestroyStompBox();
    }
    internal void DestroyStompBox()
    {
        GameObject stompbox;
        stompbox = GameObject.FindWithTag("hitbox");
        Destroy(stompbox);
    }
}