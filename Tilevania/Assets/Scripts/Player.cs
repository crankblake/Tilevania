using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    //Config
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 10f;
    [SerializeField] float climbSpeed = 5f;

    //State
    bool isAlive = true;

    //Cached component references
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    Collider2D myCollider2D;
    float myGravity;
    
    //Message then methods
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myGravity = myRigidBody.gravityScale;
        myAnimator = GetComponent<Animator>();
        myCollider2D = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        ClimbLadder();
        FlipSprite();
        Jump();
    }

    private void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxisRaw("Horizontal"); //value is between -1 to +1
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;
        //Debug.Log("Player velocity is: " + playerVelocity);

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Running", playerHasHorizontalSpeed);
    }
    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x),1f);
        }
    }
    private void Jump()
    {
        if (!myCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
            return;
        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            myRigidBody.velocity += jumpVelocityToAdd;
        }
    }
    private void ClimbLadder()
    {
        //Vector2 playerVelocity = new Vector2(myRigidBody.velocity.y, controlThrow * climbSpeed);
        //myRigidBody.velocity = playerVelocity;
        // Debug.Log("Player velocity is: " + playerVelocity);

        if (!myCollider2D.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            //myRigidBody.isKinematic = false;
            myRigidBody.gravityScale = myGravity;
            myAnimator.SetBool("Climbing", false);
            return;
        }

        float controlThrow = CrossPlatformInputManager.GetAxisRaw("Vertical");
        Vector2 climbVelocity = new Vector2(myRigidBody.velocity.x, controlThrow * climbSpeed);
        myRigidBody.velocity = climbVelocity;
        bool playerHasVerticalSpeed = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("Climbing", playerHasVerticalSpeed);

        if (controlThrow == 0)
        {
            myRigidBody.gravityScale = 0f;
            //myRigidBody.isKinematic = true;
        }
   
        /*
        Debug.Log("touching ladder");
        if (controlThrow != 0)
        {
            myRigidBody.isKinematic = true;
            climbVelocity = new Vector2(myRigidBody.velocity.x, controlThrow * climbSpeed);
            //climbVelocityToAdd = new Vector2(0f, climbSpeed);
            //myRigidBody.velocity = playerVelocity;
            myAnimator.SetBool("Climbing", playerHasVerticalSpeed);
        }
        else
        {
            myRigidBody.isKinematic = true;
            climbVelocity = new Vector2(myRigidBody.velocity.x, myRigidBody.velocity.y);
            myAnimator.SetBool("Climbing", playerHasVerticalSpeed);
        }
        //myRigidBody.velocity += climbVelocityToAdd;
        myRigidBody.velocity = climbVelocity;*/
    }
}
