using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretAI : MonoBehaviour
{
    #region Variables
    public GameObject bullet;         //Ref to the bulletPrefab ( basically the bullet we use to shoot )
    public Transform firePoint;             //Ref to the Transform component of the firePoint ( where we spawn the bullet - its a child of the AI in Hierarchy)

    private GameObject player;               //Ref to the Transform of the Player
    private SpriteRenderer myRend;          //Ref to the SpriteRenderer of the Turret, so we can flip it
    private float distance;                 //private global variable that stores the distance between the Turret and the AI
    private float shootingDistance = 10f;   //private global variable that checks at what distance from the player should the Turret start shooting ( less than 10f for us )
    #endregion

    #region MainScript
    private void Awake(){
        // Finding the Player and the SpriteRenderer
        //player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player");
        myRend = GetComponent<SpriteRenderer>();
    }
    private void Update(){
        if(player != null && !player.Equals(null)){
            distance = Vector2.Distance(player.transform.position, transform.position);       //Constantly calculating the distance
            if(distance < shootingDistance){    //Checking if the Player is in the "shootingDistance"
                AimAssist();    //We turn on the AimAssist for the AI
                if(!addedDelay){
                    StartCoroutine(AddDelay()); //Here we add the delay between shots using the AddDelay Coroutine ( search about Coroutines, its an advanced concept )
                }
            }
        }
        else{
            //ADD BEHAVIOUR
        }
    }

    #region AimAssist
    private Vector2 dir;    //A Vector2 that stores the direction from the Turret to the Player 
    private float angle;    //The angle between the Player and the Turret
    private void AimAssist(){
        //A three liner ( its an algorithm that you can find on the internet, nothing fancy)
        if(player != null && !player.Equals(null)){
            dir = transform.position - player.transform.position;
            angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
            if(angle < -90 || angle > 90){  //Here we flip the sprite according to the angle of the Turret
                myRend.flipY = true;
            }else myRend.flipY = false;
        }
    }
    #endregion

    #region Coroutine
    //Here we create a Coroutine that has a 2 seconds delay
    private bool addedDelay;
    private IEnumerator AddDelay(){
        addedDelay = true;
        int time = Random.Range(0, 4);
        yield return new WaitForSeconds( time );
        Shoot();
        addedDelay = false;
    }
    #endregion

    #region Shoot
    //Here we Instantiate the bullet at the firePoint position, but at the angle + 180 degrees rotation ( again, basic math that is found on the internet )
    private void Shoot(){
        Instantiate(bullet, firePoint.position, Quaternion.Euler(0f, 0f, angle + 180f));
    }
    #endregion
    #endregion
}