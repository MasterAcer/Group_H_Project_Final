using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    protected Rigidbody2D rb;
    public float speed;     //how fast the player will move
    public float jumpForce;     //how high the player will jump
    protected bool isGrounded = false;    //checks if player is on the ground
    public Transform isGroundedChecker;     //a transform of an empty object 
    public float groundedRadius;       //radius of the transform
    public LayerMask groundLayer;        //ground layer to check if Ground layer in under player
    public LayerMask platformLayer;      //ground layer to check if Platform layer in under player
    public float fallMultiplier;
    public float rememberGroundedFor;   //keeps us grounded longer
    protected float lastTimeGrounded;     //last time the player was on the ground
    public int defaultDoubleJump = 1;
    protected int doubleJump;
    private bool facingRight = true;
    protected int counterBlock;
    public float setJumpForce = 6;

    // Attributes of player
    public float health;
    public float maxHealth;
    public float damageDealt;
    public int numberOfShots;
    public float fireRate;
    public float timeTillRegen = 5f;

    public GameController script;
    
    // Boolean value to check if player can be moved (used upon level completion)
    protected Boolean moveable;

    public void CheckIfDead()
    {
        if (health <= 0)
        {
            script.Respawn();
        }
    }
    

    // Handles movement based on player controls
    public void Move()
    {
        float move = Input.GetAxisRaw("Horizontal");   //detects input, left = 1, right = -1
        float moveBy = move * speed;
        rb.velocity = new Vector2(moveBy, rb.velocity.y);

        if (move > 0 && !facingRight)   //flips the character when moving 
            flip();
        else if (move < 0 && facingRight)
            flip();
    }

    public void flip()  //flips the character when moving left or right
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    // Handles jump based on player controls and check if double jump is possible
    public virtual void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || Time.time - lastTimeGrounded <= rememberGroundedFor || doubleJump > 0)){
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            doubleJump--;
        }
        if (rb.velocity.y < 0) {    //making player fall faster than the speed of jumping
            rb.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
        
        }
    }

    // Check if player is touching the ground
    public void checkIfGrounded()
    {
        //initialise object collider to see if anything is colliding with the circle
        Collider2D colliderGround = Physics2D.OverlapCircle(isGroundedChecker.position, groundedRadius, groundLayer);
        Collider2D colliderPlatform = Physics2D.OverlapCircle(isGroundedChecker.position, groundedRadius, platformLayer);
        if (colliderGround != null || colliderPlatform != null) {
            isGrounded = true;
            doubleJump = defaultDoubleJump;
        }
        else{
            if (isGrounded)
            {
                lastTimeGrounded = Time.time;
            }
            isGrounded = false;
        }
    }

    // Set moveable attribute of script
    public void setMoveable(Boolean canMove)
    {
        moveable = canMove;
    }

    public void takeDamage(float damage)
    {
        if (GameObject.Find("Knight(Clone)") != null)
        {
            KnightAttack script = GameObject.Find("Knight(Clone)").GetComponent<KnightAttack>();
            if (script.getActiveBlock())
            {
                return;
            }
            else
            {
                if(timeTillRegen == 10f)  
                {
                    health -= damage;
                    regen();
                }
                else
                {
                    health -= damage;
                    timeTillRegen = 10f; 
                }
            }
        }
        else if (GameObject.Find("Rogue(Clone)") != null)
        {
            RogueAttack script = GameObject.Find("Rogue(Clone)").GetComponent<RogueAttack>();
            if (script.getActiveBlock())
            {
                return;
            }
            else
            {
                if(timeTillRegen == 10f)  
                {
                    health -= damage;
                    regen();
                }
                else
                {
                    health -= damage;
                    timeTillRegen = 10f; 
                }
            }
        }
        else if (GameObject.Find("Ranger(Clone)") != null)
        {
            RangerWeaponController script = GameObject.Find("Ranger(Clone)").GetComponent<RangerWeaponController>();
            if (script.getActiveBlock())
            {
                return;
            }
            else
            {
                if(timeTillRegen == 10f)  
                {
                    health -= damage;
                    regen();
                }
                else
                {
                    health -= damage;
                    timeTillRegen = 10f; 
                }
            }
        }
        else if (GameObject.Find("Wizard(Clone)") != null)
        {
            WizardAttack script = GameObject.Find("Wizard(Clone)").GetComponent<WizardAttack>();
            if (script.getActiveBlock())
            {
                return;
            }
            else
            {
                if(timeTillRegen == 10f)  
                {
                    health -= damage;
                    regen();
                }
                else
                {
                    health -= damage;
                    timeTillRegen = 10f; 
                }
            }
        }

    }
    
    public void regen()
    {
        if (timeTillRegen <= 0)
        {
            health += Time.deltaTime*10;
        }
        else
        {
            timeTillRegen -= Time.deltaTime;
        }
    }

}
