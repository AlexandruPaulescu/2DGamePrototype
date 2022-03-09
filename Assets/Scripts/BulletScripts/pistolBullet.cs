using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pistolBullet : MonoBehaviour
{
    #region Variables

    private float speed = 23f;       // Speed of the bullet
    private int damage = 10;         // Damage of the bullet

    private Rigidbody2D bulletRB;    // Refference to the bullet's Rigidbody2D

    [SerializeField]
    private GameObject impactEffect; //Refference to the impactEffect Prefab
    #endregion

    #region  Main Script
    void Start(){
        bulletRB = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate(){                                                   //Giving speed to the bullet
        bulletRB.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo){
        Enemy enemy = hitInfo.GetComponent<Enemy>();                //Accesing the "Enemy" script on the target we hit
        if(enemy != null){   //If there actually is an enemy script
            enemy.TakeDamage(damage);   // Dealing damage
        }

        GameObject clone = (GameObject) Instantiate(impactEffect, transform.position, transform.rotation); //Instantiating the impactEffect
        Destroy(clone, .45f);   //Destroying the effect after .45 seconds

        Destroy(gameObject);    //Destroying bullet
    }
    #endregion

    #region Comments
    /*
    *
    *   Edit Date : 4/10/2020 - 12PM
    *   Purpose : Arranging the script and adding functionality
    *   Name : Paulescu Alexandru Mohamad
    *
    *   Edit Date : 20/10/2020 - 14PM
    *   Purpose : Removing buggy functionality
    *   Name : Paulescu Alexandru Mohamad
    *
    *   GLIXEL LAB 2020
    */
    #endregion
}