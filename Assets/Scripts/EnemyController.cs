using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public float maxHealth;

    public float speed;

    public float hp;

    protected Transform playerTransform;

    private SpriteRenderer playerSpriteRender;

    protected Rigidbody2D rb;

    private SpriteRenderer sr;

    private bool detectedPlayer;  // keeps track of wether the enemy has 'seen' the player

    protected bool jump;

    public float jumpForce;

    public float fallMultiplier;

    public Transform isGroundedChecker;

    public float groundedRadius;

    public LayerMask groundLayer;        //ground layer to check if Ground layer in under player
    public LayerMask platformLayer;      //ground layer to check if Platform layer in under player

    public HealthBarBehaviour healthBar;

    private float minDetectionDistance;

    protected float rangeDistance;

    protected Vector3 prevPosition;

    // Start is called before the first frame update
    public void initialiseAttributes()
    {
        playerTransform = null;
        playerSpriteRender = null;
        detectedPlayer = false;
        jump = false;
        hp = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        hp = maxHealth;
        healthBar.setHealth(hp, maxHealth);
        minDetectionDistance = 0f;
        rangeDistance = 0f;
    }

    // Update is called once per frame 
    // Check HP every frame to make sure the object is destoryed as soon as it has no health
    void Update()
    {
        if (playerTransform != null)
        {
            checkHP();

            // If check to see if teh player has enetered enemy's 'sight'
            // Once player does, set detectedPlayer to true so that the Enemy now chases player forever
            if (detectedPlayer == false)
            {
                if (xDistanceToPlayer() < minDetectionDistance)
                {
                    detectedPlayer = true;
                }
            }

        }


    }

    // Movement is physics based so needs to be doen independant of the fps
    // Enemy will move to player if it has detecte it and if the player is a certain distance away
    public virtual void move()
    {

        float distance = xDistanceToPlayer();
        prevPosition = transform.position;

        if (distance < rangeDistance - 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(getPlayerTransform().position.x, transform.position.y), -speed * Time.deltaTime);  // Move only on the x axis
        }
        else if (distance > rangeDistance + 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(playerTransform.position.x, transform.position.y), speed * Time.deltaTime);  // Move only on the x axis

            if (jump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            }

            jump = false;
        }

        flip();

    }

    public void flip()
    {
        float movementDirection = (transform.position - prevPosition).x / Time.deltaTime;

        

        // Flip sprite based on movement
        if (movementDirection < 0 && (transform.position.x > playerTransform.position.x))
        {

            transform.localScale = new Vector2(-(Math.Abs(transform.localScale.x)), transform.localScale.y);
        }
        else if (movementDirection > 0 && transform.localScale.x < 0 && (transform.position.x < playerTransform.position.x))
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }


    // Calculates distance to player as an absolute value
    public float xDistanceToPlayer()
    {
        return Mathf.Abs(playerTransform.position.x - transform.position.x);
    }

    // A public class that can be run form other scripts for when a collision gives damage to the Enemy
    public void takeDamage(int dmgValue)
    {
        if (hp - dmgValue <= 0)  // set to 0 to ensure no neagtive hitpoints
        {
           
            hp = 0;
        }
        else
        {
            hp -= dmgValue;
        }
        healthBar.setHealth(hp, maxHealth);
    }

    // If the hitpoint of the enemy are 0 then destory it
    protected virtual void checkHP()
    {
        if (hp == 0 )
        {
            if (this.name.Equals("Enemy Boss"))
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>().setMoveable(false);
                GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().enableUpgradeMenu();
            }
            Destroy(gameObject);
        }
    }

    public void decideJump()
    {
        
        //initialise object collider to see if anything is colliding with the circle
        Collider2D colliderGround = Physics2D.OverlapCircle(isGroundedChecker.position, groundedRadius, groundLayer);
        Collider2D colliderPlatform = Physics2D.OverlapCircle(isGroundedChecker.position, groundedRadius, platformLayer);
        if (colliderGround != null || colliderPlatform != null)
        {
            if (((playerTransform.transform.position.y - playerSpriteRender.bounds.size.y) - (rb.transform.position.y - sr.size.y)) > 0.5)
            {
                jump = true;
            }

            else if (colliderPlatform != null)
            {
                if (((playerTransform.transform.position.y - playerSpriteRender.bounds.size.y) - (rb.transform.position.y - sr.size.y)) > 0)
                {
                    jump = true;
                }
                else
                {
                    jump = false;
                }
            }
            else
            {
                jump = false;
            }
        }

    }



    public void setPlayerComponents(GameObject player)
    {
        playerTransform = player.GetComponent<Transform>();
        playerSpriteRender = player.GetComponent<SpriteRenderer>();
    }

    public Transform getPlayerTransform()
    {
        return playerTransform;
    }

    public bool getDetectedPlayer()
    {
        return detectedPlayer;
    }

    public void setMinDetectionDistance(float distance)
    {
        minDetectionDistance = distance;
    }

    public void setRangeDistance(float distance)
    {
        rangeDistance = distance;
    }

    public float getHealth()
    {
        return hp;
    }

}