using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackManagement : MonoBehaviour
{
    private new Rigidbody2D rigidbody2D; //the rigidbody
    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>(); //grabs the rigidbody attatched to the gameObject
    }

    internal void knockback(Vector2 direction) //function called by other objects
    {
        rigidbody2D.velocity = new Vector2(direction.x, 5); //adds velocity in the direction given through the parameter
    }
}
