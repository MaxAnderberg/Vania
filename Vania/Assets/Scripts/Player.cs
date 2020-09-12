using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    // Config
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float climbSpeed = 5f;
    Vector3 localScale;
    

    // Chache
    Rigidbody2D myRigidbody;
    SpriteRenderer mySpriteRenderer;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeet;
    float playerGravity;

    //State
    bool isAlive = true;
    bool playerHasHorizontalSpeed;


    //Messages && Methods

    // Start is called before the first frame update
    void Start()
    {
        localScale = transform.localScale;
        myRigidbody = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeet = GetComponent<BoxCollider2D>();
        playerGravity = myRigidbody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {

        if (!isAlive){ return; }
        Run();
        FlipSprite();
        Jump();
        ClimbLadder();
        Die();
        TakeTrapDamage();
        



        // animation control center

        if (myRigidbody.velocity.y > -0.1 && myRigidbody.velocity.y < 0.1) // if the player is not moving vertically
        {
            myAnimator.SetBool("Jumping", false);
            myAnimator.SetBool("Falling", false);
        }

        if (myRigidbody.velocity.y > 0.1)
        {
            myAnimator.SetBool("Jumping", true);
        }

        if (myRigidbody.velocity.y < -0.1)
        {
            myAnimator.SetBool("Jumping", false);
            myAnimator.SetBool("Falling", true);
        }
        
        
        

    }

    private void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal"); // Value is between -1 & +1
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Running", playerHasHorizontalSpeed);
       
   
    }

    private void Jump()
    {
        if (myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"))) { 
            if (CrossPlatformInputManager.GetButtonDown("Jump"))
            {
                Vector2 jumpVelocityToAdd = new Vector2(0f, jumpForce);
                myRigidbody.velocity += jumpVelocityToAdd;

            }

        }
    }

    private void ClimbLadder()
    {
        // check so that we're touching the ladder
        var tempGravity = myRigidbody.gravityScale;
        if (myFeet.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            // I think this covers the climbing part
            float controlThrow = CrossPlatformInputManager.GetAxis("Vertical"); // Value is between -1 & +1 // Vertical instead of horizontal
            Vector2 playerClimbVelocity = new Vector2(myRigidbody.velocity.x, controlThrow * climbSpeed); // so x should be 0 and y should contain climb speed
            myRigidbody.velocity = playerClimbVelocity;

            bool playerHasVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
            myRigidbody.gravityScale = 0f;
            myAnimator.SetBool("Climbing", playerHasVerticalSpeed);
        }
        else
        {
            myRigidbody.gravityScale = playerGravity;
            myAnimator.SetBool("Climbing", false);
        }
     



    }

    private void TakeTrapDamage()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Traps")))
        {
            Debug.Log("OH YOU ARE TAKING DAMAGE!!");
            // call takePlayerHealth here pass in int as the damage
        }
    }
    private void Die()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            myAnimator.SetTrigger("Dying");
            isAlive = false; // Need to make a life class and attach it on the player
            FindObjectOfType<GameSession>().ProcessPlayerDeath();

        }
    }
    


    private void FlipSprite()
    {

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f); // this is the old way
            
        }
    }
}
