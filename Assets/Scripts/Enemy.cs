using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region Variables
    [HideInInspector]   public float health;            //The health of the enemy

    [SerializeField]    private GameObject hitEffect;        //Refferencing the "hitEffect" prefab

    #endregion

    #region Main Script

    private void Awake(){
        //PlayerPrefs.DeleteKey("health");                  //USE to reset the health player variable
        if(gameObject.tag == "Wall"){
            health = 10000000;
        }
        if(gameObject.tag == "Player"){
            if(!PlayerPrefs.HasKey("health"))
                PlayerPrefs.SetFloat("health", 100f);

            if(PlayerPrefs.GetFloat("health") <= 0)     //TEST
                PlayerPrefs.SetFloat("health", 100f);

            PlayerPrefs.GetFloat("health");
        }
        if(gameObject.name == "FaringaAI"){
            health = 100;
        }
        if(gameObject.name == "VirusTurret"){
            health = 10;
        }
    }

    public void TakeDamage(float damage){
        if(gameObject.tag != "Player"){
            health -= damage;
            if(health <= 0){
                Die();
                if(gameObject.name == "FaringaAI"){
                }
                else if(gameObject.name == "VirusTurret"){
                    
                }
            }
        }
        else{
            PlayerPrefs.SetFloat("health", PlayerPrefs.GetFloat("health") - damage);
            if(PlayerPrefs.GetFloat("health") <= 0){
                Die();
            }
        }
        
        //Remove the IF if we will have different death effects
    }
    void Die(){;
        GameObject clone = (GameObject) Instantiate(hitEffect, transform.position, transform.rotation);
        Destroy(clone, .4f);

        if(gameObject.tag != "Player"){
            gameObject.SetActive(false);
        }
        else{
            Destroy(gameObject);
        }
        
    }
    #endregion

    #region Comments
    /*
    *
    *   Last Edit Date : 4/10/2020
    *   Purpose : Arranging the script
    *   Name : Paulescu Alexandru Mohamad
    *
    *   Last Edit Date : 20/10/2020
    *   Purpose : Arranging the script and commenting the actual functionality
    *   Name : Paulescu Alexandru Mohamad
    *
    *   Last Edit Date : 8/11/2020
    *   Purpose : Added Health system for each Enemy and special system for Player's Health and also saving it
    *   Name : Paulescu Alexandru Mohamad
    *
    *   GLIXEL LAB 2020
    */
    #endregion
}