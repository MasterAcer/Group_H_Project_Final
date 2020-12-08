using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyController : EnemyController
{

    private Animator weaponAnimator;
    // Start is called before the first frame update
    void Start()
    {

        initialiseAttributes();
        setMinDetectionDistance(4f);
        setRangeDistance(0.3f);
        weaponAnimator = GetComponentInChildren<Animator>();


    }



    // Movement is physics based so needs to be doen independant of the fps
    // Enemy will move to player if it has detecte it and if the player is a certain distance away
    private void FixedUpdate()
    {

        if (getPlayerTransform() != null)
        {
            if (getDetectedPlayer())
            {
                move();


                useWeapon();



            }

        }

    }

    private void useWeapon()
    {
        if (xDistanceToPlayer() > 0.5f)
        {
            weaponAnimator.SetBool("useWeapon", false);
        }
        else
        {
            weaponAnimator.SetBool("useWeapon", true);
        }

    }


}