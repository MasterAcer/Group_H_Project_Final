using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyWeaponController : MonoBehaviour
{

    public float damagePerHit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // When the weapon collides with a Player, apply damage to it
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<CharacterController>().takeDamage(damagePerHit);
        }
    }
}
