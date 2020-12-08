using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardShotBehaviour : MonoBehaviour
{

    public Transform areaOfEffect; 
    public float shotRange;
    public LayerMask enemies;

    // Amount of damage each shot does
    public int damagePerShot;

    // Speed of shot
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = GameObject.FindGameObjectWithTag("Shot Spawn Point").GetComponent<Transform>().up * speed;
    }

    // When collided with an object, if it is Enemy then apply damamge, and delete the shot
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy" || col.gameObject.name == "Ground" )
        {
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(areaOfEffect.position, shotRange, enemies);  //All enemies in this radius are damaged
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<EnemyController>().takeDamage(damagePerShot);  //Uses the takeDamage function when the enemy is hit
            }
            Destroy(gameObject);
        }
        
    }

    private void OnDrawGizmosSelected() //Draws the gizmos which helps us visualise the attack radius in unity
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(areaOfEffect.position, shotRange);
    }
}
