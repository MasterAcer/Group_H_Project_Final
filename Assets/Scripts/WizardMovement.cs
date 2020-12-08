using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WizardMovement : CharacterController
{

    public Text t;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
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
        CheckIfDead();
        regen();
        if(health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public override void Jump()
    {
        GetComponent<Rigidbody2D>().gravityScale = 1;
        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || Time.time - lastTimeGrounded <= rememberGroundedFor))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            
        }

        if (rb.velocity.y < 0 && (!Input.GetKey(KeyCode.Space)))
        {   
            rb.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;

        }

        if (Input.GetKey(KeyCode.Space) && (!isGrounded) && rb.velocity.y < -0.1)
        {
            GetComponent<Rigidbody2D>().gravityScale = 0.5f;
        }
    }
}
