using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RangerMovement : CharacterController
{
    public Text t;
    public bool wallJumping;
    private WallCollision wallCollision;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        wallCollision = GetComponent<WallCollision>();
        moveable = true;
        rb = GetComponent<Rigidbody2D>();
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
        wallJump();
        CheckIfDead();
        regen();
        if(health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public void wallJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && wallCollision.onWall && !isGrounded)
        {
            wallJumping = true;

        }
        else
        {
            wallJumping = false;
        }
        if (wallJumping)
        {

            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
}
