using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//------------------------------------------------------------------------
// Main Player Controller
//------------------------------------------------------------------------
public class PlayerController : MonoBehaviour
{
    // Reference to the sub scripts
    [SerializeField]
    internal PlayerInputScript inputScript;

    [SerializeField]
    internal PlayerMovementScript movementScript;

    [SerializeField]
    internal PlayerCollisionScript collisionScript;

    [SerializeField]
    internal PlayerAbilityScript abilityScript;

    [SerializeField]
    internal PlayerAnimationScript animationScript;

    [SerializeField]
    internal PlayerUIScript UIScript;

    [SerializeField]
    internal Transform groundcheckL;
    [SerializeField]
    internal Transform groundcheckL2;
    [SerializeField]
    internal Transform groundcheckR;
    [SerializeField]
    internal Transform groundcheckR2;

    [SerializeField]
    internal Transform wallcheckL;
    [SerializeField]
    internal Transform wallcheckL2;
    [SerializeField]
    internal Transform wallcheckR;
    [SerializeField]
    internal Transform wallcheckR2;

    [SerializeField]
    internal PlayerTalents talents;

    [SerializeField]
    internal OxygenCounter oxygenCounter;

    //Audio
    [SerializeField]
    internal AudioSource audioSource;
    [SerializeField]
    internal AudioClip dashClip;
    [SerializeField]
    internal AudioClip deathClip;
    [SerializeField]
    internal AudioClip jumpClip;
    [SerializeField]
    internal AudioClip shieldPopClip;
    [SerializeField]
    internal AudioClip hitClip;

    //Reference to components
    internal Rigidbody2D rb2d;
    internal Vector2 lastCheckpoint;
    internal int checkpointOxygen;

    private void Awake()
    {
        //Get the rigidbody of the player
        rb2d = GetComponent<Rigidbody2D>();
        //Save the starting position as last checkpoint
        lastCheckpoint = transform.position;
    }    
}
