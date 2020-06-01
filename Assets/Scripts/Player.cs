﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    //Config
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;

    //State
    bool isAlive = true;

    //cached component references
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    Collider2D myCollider2d;

    //messages, then methods

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider2d = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        FlipSprite();
        Jump();
        ClimbLadder();
    }

    private void Run()
    {
        //the cross platform input manager lets us make our games for different devices
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal"); //-1 to +1
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;

        Debug.Log("player velocity :" + playerVelocity);

        myAnimator.SetBool("running", PlayerHasHorizontalSpeed());
    }

    private void ClimbLadder()
    {
        if (!myCollider2d.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            return;
        }

        float controlFlow = CrossPlatformInputManager.GetAxis("Vertical");

        Vector2 climbVelocity = new Vector2(myRigidBody.velocity.x, controlFlow * climbSpeed);

        myRigidBody.velocity = climbVelocity;

        bool playerHasVerticalSpeed = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;

        myAnimator.SetBool("climbing", playerHasVerticalSpeed);

    }

    private void Jump()
    {

        if(! myCollider2d.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }

        if (CrossPlatformInputManager.GetButtonDown("Jump")) //from project settings -> input
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            myRigidBody.velocity += jumpVelocityToAdd;
        }
    }

    private void FlipSprite()
    {
        //if player moves horizontally

        
        bool playerHasHorizonalSpeed = PlayerHasHorizontalSpeed();

        if (playerHasHorizonalSpeed)
        {
            //reverse current scaling of x axis

            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f); //uses the velocity on x (so it will be positive if going right, negative if going left), then setting that to the localScale
        }


    }

    private bool PlayerHasHorizontalSpeed()
    {
        //mathf.Abs - if float is +ve or 0, returns 1, else returns -1
        return Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
    }
}
