using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//------------------------------------------------------------------------
// Player Animation Script
//------------------------------------------------------------------------
public class PlayerAnimationScript : MonoBehaviour
{
    //Player Controller Reference
    [SerializeField]
    internal PlayerController playerController;

    [SerializeField]
    internal Animator animator;
    [SerializeField]
    internal Animator shieldAnimator;
    [SerializeField]
    internal GameObject deathMenu;

    //PlayerAnimationScript Variables
    private bool lookingLeft = false;
    private bool lookingRight = true;
    internal bool playAttack = false;
    internal bool playJump = false;
    internal bool playDash = false;
    internal bool playLanding = false;
    internal bool playWalled = false;
    internal bool playHit = false;
    internal bool playDead = false;
    internal bool Shield = false;

    //Death animation clip for length
    [SerializeField]
    internal AnimationClip deadAnimationClip;
    [SerializeField]
    internal AnimationClip destroyShieldClip;

    // Update is called once per frame
    void Update()
    {   
        //Get Talent integers
        animator.SetInteger("offenseTalent",playerController.talents.Offensive);
        //Change direction depending on Input
        if(playerController.inputScript.isLeft)
        {
            turnLeft();
        }
        else if(playerController.inputScript.isRight)
        {
            turnRight();
        }        
        //Attack Animation
        if(playAttack)
        {
            attackAnimation();
        }   
        //jump Animation
        if(playJump)
        {
            jumpAnimation();
        }
        //Dash Animation
        if(playDash)
        {
            startDashAnimation();
        }
        else if(!playDash)
        {
            stopDashAnimation();
        }
        //Landing Animation
        if(playLanding)
        {
            landingAnimation();
        }
        //Wall Climbing Animations
        if(playWalled)
        {
            walledAnimation();
        }
        else if(!playWalled)
        {
            stopWalledAnimation();
        }
        //Getting hit animation
        if(playHit)
        {
            hitAnimation();
        }
        //Death animation
        if(playDead)
        {
            StartCoroutine(deadAnimation());
        }

        //Shield
        if(Shield)
        {
            //Activate the GameObject Shield
            shieldAnimator.gameObject.SetActive(true);
            //Set Boolean to true
            shieldAnimator.SetBool("isShielded", true);
        }
        else if(!Shield && shieldAnimator.gameObject.activeSelf)
        {
            StartCoroutine(destroyShield());            
        }
    }
    private void turnLeft()
    {
        //Change booleans when looking left
        lookingRight = false;
        lookingLeft = true;
    }
    private void turnRight()
    {
        //Change booleans when looking right
        lookingLeft = false;
        lookingRight = true;
    }
    private void attackAnimation()
    {
        //Send the correct Trigger
        if (lookingRight)
        {
            animator.SetTrigger("isRAtt");
        }
        else if (lookingLeft)
        {
            animator.SetTrigger("isLAtt");
        }
        //Reset the animation boolean
        playAttack = false;
    }
    private void jumpAnimation()
    {
        //Send Trigger
        animator.SetTrigger("isJump");
        //Reset Boolean
        playJump = false;
    }

    private void startDashAnimation()
    {
        //Set the correct Animator Boolean depending of Side
        if (lookingRight)
        {
            animator.SetBool("isRDash",true);
        }
        else if (lookingLeft)
        {
            animator.SetBool("isLDash",true);
        }
        //Send the Trigger for the animation
        animator.SetTrigger("DashTrigger");
    }
    private void stopDashAnimation()
    {
        //Reset animator Booleans to start the exit animation
        animator.SetBool("isRDash", false);
        animator.SetBool("isLDash", false);
    }
    private void landingAnimation()
    {
        //Send Trigger
        animator.SetTrigger("isLanding");
        //Reset Boolean
        playLanding = false;
    }

    private void walledAnimation()
    {
        //Set the correct Animator Boolean depending of Side
        if (lookingRight)
        {
            animator.SetBool("isRWall", true);
        }
        else if (lookingLeft)
        {
            animator.SetBool("isLWall", true);
        }
    }
    private void stopWalledAnimation()
    {
        //Reset Animator Booleans to start excit animation
        animator.SetBool("isRWall", false);
        animator.SetBool("isLWall", false);
    }
    private void hitAnimation()
    {
        Debug.Log("Oh no I got hit");
        //Send Trigger
        animator.SetTrigger("isHit");
        //Reset Boolean
        playHit = false;
    }
    IEnumerator deadAnimation()
    {
        //Reset Boolean
        playDead = false;
        //Send Trigger
        animator.SetTrigger("isDead");
        //Wait Length of death animation
        yield return new WaitForSeconds(deadAnimationClip.length);
        //deactivate the Player
        gameObject.SetActive(false);
        //activate death Menu
        deathMenu.SetActive(true);
        //Stop time
        Time.timeScale = 0f;
    }

    IEnumerator destroyShield()
    {
        //Reset Boolean
        shieldAnimator.SetBool("isShielded", false);
        //Wait
        yield return new WaitForSeconds(destroyShieldClip.length);
        //Set active to false
        shieldAnimator.gameObject.SetActive(false);
    }
}

