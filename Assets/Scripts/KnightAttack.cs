using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightAttack : MonoBehaviour
{
    public float timeBtwAttack = 0;    //These 2 variables are so the player does not spam attack
    public float startTimeBtwAttack;    //("Time between attack")
    public float timeBtwShield = 0;
    public float startTimeBtwShield;

    public Transform attackPos; //Position of radius
    public float attackRange;   //Range of the attack
    public LayerMask whatIsEnemies; //Layer to know what are enemies
    public int damage;
    public GameObject shield;
    private bool activeBlock;
    public GameObject sword;
    private bool activeSword;

    private CharacterController knight;

    public Animator playerAnim;


    void Start()    //at the start sword must show and shield must not
    {
        knight = GetComponent<CharacterController>();
        activeSword = true;
        sword.GetComponent<Renderer>().enabled = true;
        activeBlock = false;
        shield.GetComponent<Renderer>().enabled = false;
    }

    void Update()
    {
        Attack();
        Shield();

    }

    public void Attack()
    {
        if (timeBtwAttack <= 0) //Only allows attack after cooldown
        {
            if (Input.GetMouseButtonDown(0) && activeSword)
            {
                playerAnim.SetTrigger("attack");
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);  //All enemies in this radius are damaged
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<EnemyController>().takeDamage(damage);  //Uses the takeDamage function when the enemy is hit
                }
                timeBtwAttack = startTimeBtwAttack;
            }

        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    public void Shield()    //player takes no damage when shield is up, delay after the shield is up.
    {
        if (Input.GetMouseButton(1))    //if right mouse button is clicked, show the shield, remove the sword, and dont take any damage.                    
        {                               //When let go of the shield, sword shows again, and timer starts till you can put shield up again.
            if (timeBtwShield <= 0)
            {
                timeBtwShield = startTimeBtwShield;
                shield.GetComponent<Renderer>().enabled = true;
                activeBlock = true;
                sword.GetComponent<Renderer>().enabled = false;
                activeSword = false;
                knight.speed = 2.25f;
                knight.jumpForce = 1;
            }
        }
        else
        {
            activeBlock = false;
            shield.GetComponent<Renderer>().enabled = false;
            sword.GetComponent<Renderer>().enabled = true;
            activeSword = true;
            timeBtwShield -= Time.deltaTime;
            knight.speed = 4.5f;
            knight.jumpForce = 7;
        }
    }

    public bool getActiveBlock()
    {
        return activeBlock;
    }

    private void OnDrawGizmosSelected() //Draws the gizmos which helps us visualise the attack radius in unity
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
 
}
