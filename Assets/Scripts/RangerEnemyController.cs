using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerEnemyController : EnemyController
{

    private bool canShoot;

    public float fireRate; // rate at which shots are fired
    private float nextFire; // delay between next shot

    private Vector2 lookDirection;
    private float lookAngle;

    public GameObject shot; // shot object
    public Transform shotSpawnPoint; // spawn point for shots
    
    // distance at which ranged enemy starts to back away from player
    private float retreatDistance;


    // Start is called before the first frame update
    void Start()
    {
        initialiseAttributes();
        canShoot = false;
        setMinDetectionDistance(5f);
        setRangeDistance(6f);
        retreatDistance = 2f;
    }



    // Movement is physics based so needs to be done independant of the fps
    // Enemy will move to player if it has detecte it and if the player is a certain distance away
    private void FixedUpdate()
    {

        if (getPlayerTransform() != null)
        {
            if (getDetectedPlayer())
            {

                move();

                shoot();

            }

        }

    }
    
    
    // Overridden for different move logic for ranged enemies
    public override void move()
    {

        float distanceX = xDistanceToPlayer();
        float distanceY = yDistanceToPlayer();
        prevPosition = transform.position;

        if (distanceX < retreatDistance && distanceY < 0.2f)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(getPlayerTransform().position.x, transform.position.y), -speed * Time.deltaTime);  // Move only on the x axis
        }
        else if (distanceX > rangeDistance)
        {

            canShoot = false;
            
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(playerTransform.position.x, transform.position.y), speed * Time.deltaTime);  // Move only on the x axis

            if (jump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            }

            jump = false;
        }

        flip();

    }
    
    // Calculate y distance to player
    public float yDistanceToPlayer()
    {
        return Mathf.Abs(playerTransform.position.y - transform.position.y);
    }

    public void shoot()
    {
        if (xDistanceToPlayer() < rangeDistance && yDistanceToPlayer() < rangeDistance)
        {
            canShoot = true;

        }

        if (canShoot)
        {
            if (Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;

                Instantiate(shot, shotSpawnPoint.position, shotSpawnPoint.rotation);


            }
        }
    }

    

}