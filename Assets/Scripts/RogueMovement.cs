using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RogueMovement : CharacterController
{
    public Text t;
    public bool wallClimb;
    public float climbSpeed;
    private WallCollision wallCollision;    //WallCollison object

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        wallCollision = GetComponent<WallCollision>();
        moveable = true;
        script = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        t.text = "Health: " + Mathf.Round(health) + "/" + maxHealth;
        if (moveable)
        {
            Move();
            Jump();

        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        checkIfGrounded();
        wallClimbing();
        CheckIfDead();
        regen();
        if(health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public void wallClimbing() 
    {
        if (wallCollision.onWall == true) //If on a wall then wallClimb = true, if not wallClimb = false
        {
            wallClimb = true;
        }
        else
        {
            wallClimb = false;
        }
        if (wallClimb == true)  //If wallclimb = true, make gravity 0 so it doesnt fall and make y axis 0 so it doesnt move
        {
            rb.gravityScale = 0.0f; //Changes gravity to 0 so player does not move
            rb.velocity = new Vector2(rb.velocity.x, 0);    //Changes velocity of y to 0 so it doesn't fly away
            rb.velocity = new Vector2(rb.velocity.x, Input.GetAxis("Vertical") * climbSpeed); //Changes velocity of y to make it move when climbing up or down
        }
        else
        {
            rb.gravityScale = 1.7f; //Change gravity back when off the wall
        }
    }

}
