using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossEnemyController : EnemyController
{

    private bool canShoot;

    public float fireRate; // rate at which shots are fired
    private float nextFire; // delay between next shot

    private Vector2 lookDirection;
    private float lookAngle;

    public GameObject shot; // shot object
    public Transform shotSpawnPoint; // spawn point for shots

    private Animator weaponAnimator;



    // Start is called before the first frame update
    void Start()
    {
        initialiseAttributes();
        canShoot = false;
        setMinDetectionDistance(100f);
        setRangeDistance(0.6f);
        weaponAnimator = GetComponentInChildren<Animator>();
    }



    // Movement is physics based so needs to be doen independant of the fps
    // Enemy will move to player if it has detecte it and if the player is a certain distance away
    private void FixedUpdate()
    {


        if (getPlayerTransform() != null)
        {
            move();

            if (xDistanceToPlayer() > 1.2f)
            {

                shoot();
                weaponAnimator.SetBool("useWeapon", false);
            }
            else
            {

                weaponAnimator.SetBool("useWeapon", true);





            }
        }

    }

    public void shoot()
    {
        if (xDistanceToPlayer() < 7f)
        {
            canShoot = true;

        }

        if (canShoot)
        {
            if (Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;

                // Rotate shot spawn point relative to mouse position
                lookDirection = getPlayerTransform().position - transform.position;
                lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
                shotSpawnPoint.rotation = Quaternion.Euler(0f, 0f, lookAngle - 90f);

                Instantiate(shot, shotSpawnPoint.position, shotSpawnPoint.rotation);


            }
        }
    }

    




}