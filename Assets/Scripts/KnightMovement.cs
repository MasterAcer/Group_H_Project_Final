using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KnightMovement : CharacterController
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

}
