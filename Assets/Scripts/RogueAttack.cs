using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueAttack : MonoBehaviour
{
    public Transform attackPos;
    public float attackRange;
    public LayerMask whatIsEnemies;
    public int damage;
    public float timeBtwAttack = 0;
    public float startTimeBtwAttack;

    private CharacterController rogue; 
    private bool activeBlock;
    private float timeCounter = 0.5f;

    SpriteRenderer SpriteRen;
    Color temp;

    public Animator playerAnim;
    // Start is called before the first frame update
    void Start()
    {
        rogue = GetComponent<CharacterController>();
        SpriteRen = GetComponent<SpriteRenderer>();
        temp = SpriteRen.GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
        Counter();
    }

    public void Attack()
    {
        if (timeBtwAttack <= 0) //Only allows attack after cooldown
        {
            if (Input.GetMouseButtonDown(0))
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
    private void OnDrawGizmosSelected() //Draws the gizmos which helps us visualise the attack radius in unity
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    public void Counter()   //If right mouse button is clicked, active counter is on
    {
        if (Input.GetMouseButtonDown(1))
        {
            activeBlock = true;
            temp.a = 0.8f;
            SpriteRen.GetComponent<SpriteRenderer>().color = temp;
            Invoke("Countered", timeCounter);   //Makes counter work more often and feels better
        }

    }

    void Countered()
    {
        activeBlock = false;
        temp.a = 1f;
        SpriteRen.GetComponent<SpriteRenderer>().color = temp;
    }
    public bool getActiveBlock()
    {
        return activeBlock;
    }
}
