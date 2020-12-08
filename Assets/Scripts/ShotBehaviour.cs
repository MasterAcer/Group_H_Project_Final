﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotBehaviour : MonoBehaviour
{

    
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
        if (col.gameObject.CompareTag("Enemy"))
        {
            col.gameObject.GetComponent<EnemyController>().takeDamage(damagePerShot);
        }
        Destroy(gameObject);
    }
}
