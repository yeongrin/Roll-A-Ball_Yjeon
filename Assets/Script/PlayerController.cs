using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    public float speed = 1.0f;  
    public int pickupCount;     
    private bool wonGame = false;

    void Start()
    {
        //Get the rigidbody component attached to this game object
        rb = GetComponent<Rigidbody>();
        //Work out how many pickups are in the scene and store in (pickupCount)
        pickupCount = GameObject.FindGameObjectsWithTag("Pickup").Length;
        //Desplay the pickups to the user
        CheckPickups();
    }

    void FixedUpdate()
    {
        //If we have won the game, return from the fuction
        if (wonGame)
            return;

            //Store the horizontal axis value in a float
            float moveHorizontal = Input.GetAxis("Horizontal");
            //Store the vertical axis value in a float
            float moveVertical = Input.GetAxis("Vertical");

            //Create a new vector 3 based on the horizontal and vertical values
            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

            //Add force to out rigidbody from our movement vector time our speed 
            rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        //if we collide with a pickup, destroy the pickup
        if (other.gameObject.CompareTag("Pickup"))
        {
            //Decrenebt the pickupCount when we collide with a pickup
            pickupCount -= 1;
            //Desplay the pickups to the user
            CheckPickups();

            Destroy(other.gameObject);
        }
    }

    void CheckPickups()
    {
        //Display the new pickup count to the player
        Debug.Log("Pickup Count: " + pickupCount);
        //Check if the pickupCount == 0 
        if (pickupCount == 0)
        {
            //if pickupCound == 0, disply win message, 
            Debug.Log("You Win");
            //remove controls from player
            wonGame = true;
            //Set the velocity of the rigidbody to zero
            rb.velocity = Vector3.zero;
        }
    }
}
