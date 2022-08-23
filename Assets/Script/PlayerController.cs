using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 1.0f;
    private int count;
    private int pickupCount; //The number of pickups in out scene
    //Controllers
    SoundController soundController;
    GameObject resetPoint;
    bool resetting = false;
    Color originalColour;
    int totalPickups;
    private bool wonGame = false;
    [Header("UI Stuff")]
    public GameObject gameOverScreen;
    public TMP_Text countText;
    public TMP_Text scoreText;
    public TMP_Text WinText;
    public TMP_Text WinText2;
    public GameObject inGamePanel;
    public GameObject winPanel;
    public Image pickupFill;
    public int JumpPower;
    public int MoveSpeed;
    float pickupChunk;
    bool grounded = true;

    void Start()
    {
        //Get the rigidbody component attached to this game object
        rb = GetComponent<Rigidbody>();
        count = 0;
        pickupCount = GameObject.FindGameObjectsWithTag("Pickup").Length;
        gameOverScreen.SetActive(false); //Turn off out game over screen at start of game
        SetCountText();
        WinText.text = "";
        WinText2.text = "";
        soundController = FindObjectOfType<SoundController>();
        resetPoint = GameObject.Find("Reset Point");
        originalColour = GetComponent<Renderer>().material.color;
        //Turn off our win panel object
        winPanel.SetActive(false);
        //Turn on our in game panel
        inGamePanel.SetActive(true);
        //Work out how many pickups are in the scene and store in (pickupCount)
        // Assign the amount of pickups to the total pickups
        totalPickups = pickupCount;
        //Work out the amount of fill for our pickup fill
        pickupChunk = 1.0f / totalPickups;
        pickupFill.fillAmount = 0;
        //Desplay the pickups to the user
        CheckPickups();
    }

    void Update()
    {
        Jump();
    }
    
    void FixedUpdate()
    { //If we have won the game, return from the fuction
       if (resetting)
           return;

       if (grounded)
        {
        //Store the horizontal axis value in a float
            float moveHorizontal = Input.GetAxis("Horizontal");
            //Store the vertical axis value in a float
            float moveVertical = Input.GetAxis("Vertical");

            //Create a new vector 3 based on the horizontal and vertical values
            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

            //Add force to out rigidbody from our movement vector time our speed 
            rb.AddForce(movement * speed);
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //if we collide with a pickup, destroy the pickup
        if(other.gameObject.CompareTag("Pickup"))
        {
            other.GetComponent<Particles>().CreateParticles();
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
            soundController.PlayPickupSound();
            //Decrenebt the pickupCount when we collide with a pickup
            pickupCount -= 1;
            //Desplay the pickups to the user
            pickupFill.fillAmount = pickupFill.fillAmount + pickupChunk;
            CheckPickups();   

            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            soundController.PlayCollisionSound(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Respawn"))
        {
            StartCoroutine(ResetPlayer());
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
            grounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
            grounded = false;
    }


    public IEnumerator ResetPlayer()
    {
        resetting = true;
        GetComponent<Renderer>().material.color = Color.black;
        rb.velocity = Vector3.zero;
        Vector3 startPos = transform.position;
        float resetSpeed = 1f;
        var i = 0.0f;
        var rate = 1.0f / resetSpeed;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            transform.position = Vector3.Lerp(startPos, resetPoint.transform.position, i);
            yield return null;
        }
        GetComponent<Renderer>().material.color = originalColour;
        resetting = false;
    }
    
    void CheckPickups()
    {
        //Display the new pickup count to the player
        scoreText.text = "Pickups Left: " + pickupCount.ToString() + "/" + totalPickups.ToString();
        //Increase the fill amount of our pickup fill image
       
        //Check if the pickupCount == 0 
        if (pickupCount == 0)
        {
            //if pickupCound == 0, disply win message, 
            winPanel.SetActive(true);
            //Turn off our in game pannel
            inGamePanel.SetActive(false);
            //remove controls from player
            wonGame = true;
            //Set the velocity of the rigidbody to zero
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if(count >= pickupCount)
        { 
            void WinGame()
            {
                gameOverScreen.SetActive(true); //Turns on out Game Over Screen
                WinText.text = "Game is Done!";
                WinText2.text = "Snack time is OVER!";
                soundController.PlayWinSound();
            }
        }
    }
    
    //Temporary reset funtionality
    public void ResetGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene
         (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
