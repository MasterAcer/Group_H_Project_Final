using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightWeaponController : MonoBehaviour
{

    // Amount of damage each hit does
    private int damagePerHit;
    
    // Animator component of weapon
    private Animator weaponAnimation;

    // Start is called before the first frame update
    void Start()
    {
        weaponAnimation = GetComponent<Animator>();
        damagePerHit = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            weaponAnimation.SetBool("useWeapon", true);
        }
        else
        {
            weaponAnimation.SetBool("useWeapon", false);
        }
    }
    
    
    // When the weapon collides with an object, if it is Enemy (it should only be enemy due to collision layers), apply damage to it
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ranger Enemy")
        {
            col.gameObject.GetComponent<RangerEnemyController>().takeDamage(damagePerHit);
        }

        else if (col.gameObject.tag == "Melee Enemy")
        {
            col.gameObject.GetComponent<MeleeEnemyController>().takeDamage(damagePerHit);
        }
    }
}
