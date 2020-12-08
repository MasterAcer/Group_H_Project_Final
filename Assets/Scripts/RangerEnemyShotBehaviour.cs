using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerEnemyShotBehaviour : MonoBehaviour
{
    // Amount of damage each shot does
    public float damagePerShot;

    // Speed of shot
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 lookDirection = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;
        float lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, lookAngle - 90f);
        
        GetComponent<Rigidbody2D>().velocity = transform.up * speed;
    }

    // When collided with an object, if it is Enemy then apply damage, and delete the shot
    void OnCollisionEnter2D(Collision2D col)
    {
        Destroy(gameObject);
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<CharacterController>().takeDamage(damagePerShot);
        }
    }

}
