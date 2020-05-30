using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f;

    Rigidbody2D myRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        FlipSprite();
    }

    private void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal"); //-1 to +1
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;
    }

    private void FlipSprite()
    {
        //if player moves horizontally

        //mathf.Abs - if float is +ve or 0, returns 1, else returns -1
        bool playerHasHorizonalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizonalSpeed)
        {
            //reverse current scaling of x axis

            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f); //uses the velocity on x (so it will be positive if going right, negative if going left), then setting that to the localScale
        }


    }
}
