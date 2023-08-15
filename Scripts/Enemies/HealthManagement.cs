using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManagement : MonoBehaviour
{
    //Variables
    public int MaxHP = 100;
    [SerializeField]
    private int currentHP;
    [SerializeField]
    private GameObject gate;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private AnimationClip deathAnimationClip;

    //Audio
    [SerializeField]
    internal AudioSource audiosource;
    [SerializeField]
    internal AudioClip enemyDeathClip;
    // Start is called before the first frame update
    void Start()
    {
        //Set HP at start
        currentHP = MaxHP;
    }

    private void Update()
    {
        //Check if Player died to reset HP
        if (player.activeSelf == false)
        {
            currentHP = MaxHP;
        }
    }
    public void takeDamage(int damageTaken)
    {        
        //Reduce Hp by damage amount
        currentHP -= damageTaken;
        Debug.Log("Dealing Damage");
        animator.SetTrigger("isHit");

        if (currentHP <= 0)
        {   if(gate != null)
            {
                //Trigger Gate if necessary
                gate.GetComponent<gateManager>().openTrigger = true;
            }
            //Kill Enemy
            StartCoroutine(killEnemy());           
        }
    }

    IEnumerator killEnemy()
    {
        //Play Enemy Death Clip
        audiosource.PlayOneShot(enemyDeathClip);
        //Deactivate Collider
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        //Play Death Animation
        animator.SetTrigger("isDead");
        //Wait
        yield return new WaitForSeconds(deathAnimationClip.length);
        //Destroy GameObject
        Destroy(gameObject);
    }
}
