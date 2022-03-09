using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaringaAI : MonoBehaviour
{
    #region Variables
    //public Transform target;        //Ref to the Player

    public Transform firePointNW;
    public Transform firePointNE;
    public Transform firePointSE;
    public Transform firePointSW;

    public GameObject bullet;

    private Rigidbody2D rb;         //Ref to the AI's Rigidbody2D
    private Animator ar;

    private float angle;            //Variable that stores the angle between the AI and the player
    private float distance;         //Variable that stores the distance between the AI and the player
    #region DirectionVectors
    private Vector2 Offset;
    //-------------------------------------------
    private Vector2 NW = new Vector2(-2f, 2f);
    private Vector2 NE = new Vector2(2f, 2f);
    private Vector2 SW = new Vector2(-2f, -2f);
    private Vector2 SE = new Vector2(2f, -2f);
    //-------------------------------------------
    #endregion

    private float timeLeft;
    private float desiredDirection;
    private bool hasStopped = true;
    #endregion

    private void Start(){
        ar = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        timeLeft = Random.Range(0,3);
    }
    private void FixedUpdate(){
        timeLeft -= Time.deltaTime;
        if(hasStopped == true){                         //This "Faringa" script only uses 4 directions due to its body design
                desiredDirection = Random.Range(0, 4);
                if(desiredDirection == 0){
                    Offset = NW;
                    ar.SetBool("isNW", true);
                    ar.SetBool("isNE", false);
                    ar.SetBool("isSE", false);
                    ar.SetBool("isSW", false);
                }
                else if(desiredDirection == 1){
                    Offset = NE;
                    ar.SetBool("isNE", true);
                    ar.SetBool("isNW", false);
                    ar.SetBool("isSE", false);
                    ar.SetBool("isSW", false);
                }
                else if(desiredDirection == 2){
                    Offset = SE;
                    ar.SetBool("isSE", true);
                    ar.SetBool("isNE", false);
                    ar.SetBool("isSW", false);
                    ar.SetBool("isNW", false);
                }
                else if(desiredDirection == 3){
                    Offset = SW;
                    ar.SetBool("isSW", true);
                    ar.SetBool("isNE", false);
                    ar.SetBool("isNW", false);
                    ar.SetBool("isSE", false);
                }
        }

        if(timeLeft < 0 && !bulletDelay){
            StartCoroutine(BulletDelay());
        }

        if(timeLeft >= 0){              //Inbetween WalkTimes. Basically when the AI is not moving at all, we set the Speed ANIM value to 0
            ar.SetFloat("Speed", 0);
        }
        if(timeLeft < 0 && collided == false){               //During the WalkTime. Basically when the AI moves, we set the speed to the desired value ( 100 in our case )
            hasStopped = false;
            ar.SetFloat("Speed", 100);
            rb.MovePosition(rb.position + Offset * .75f * Time.deltaTime);   //We move the player 
        }
        if(timeLeft < 0 && !addedDelay)            //We call the waitDelay ( the Coroutine that has a random delay)
            StartCoroutine(RandDelay());
        if(collided == true){                           //If the AI collided
            if(desiredDirection == 0){
                rb.MovePosition(rb.position + SE * 1f * Time.deltaTime);
                ar.SetBool("isSE", true);
                ar.SetBool("isNE", false);
                ar.SetBool("isSW", false);
                ar.SetBool("isNW", false);
            }
            else if(desiredDirection == 1){
                rb.MovePosition(rb.position + SW * 1f * Time.deltaTime);
                ar.SetBool("isSW", true);
                ar.SetBool("isNE", false);
                ar.SetBool("isNW", false);
                ar.SetBool("isSE", false);
            }
            else if(desiredDirection == 2){
                rb.MovePosition(rb.position + NW * 1f * Time.deltaTime);
                ar.SetBool("isNW", true);
                ar.SetBool("isNE", false);
                ar.SetBool("isSE", false);
                ar.SetBool("isSW", false);
            }
            else if(desiredDirection == 3){
                rb.MovePosition(rb.position + NE * 1f * Time.deltaTime);
                ar.SetBool("isNE", true);
                ar.SetBool("isNW", false);
                ar.SetBool("isSE", false);
                ar.SetBool("isSW", false);
            }
        }
    }

    private void Shoot(){
        if(desiredDirection == 0 && collided == false){
            Instantiate(bullet, transform.position, firePointNW.rotation);
        }
        else if(desiredDirection == 1 && collided == false){
            Instantiate(bullet, transform.position, firePointNE.rotation);
        }
        else if(desiredDirection == 2 && collided == false){
            Instantiate(bullet, transform.position, firePointSE.rotation);
        }
        else if(desiredDirection == 3 && collided == false){
            Instantiate(bullet, transform.position, firePointSW.rotation);
        }

        if(collided == true){
            if(desiredDirection == 0){
                Instantiate(bullet, transform.position, firePointSE.rotation);
            }
            else if(desiredDirection == 1){
                Instantiate(bullet, transform.position, firePointSW.rotation);
            }
            else if(desiredDirection == 2){
                Instantiate(bullet, transform.position, firePointNW.rotation);
            }
            else if(desiredDirection == 3){
                Instantiate(bullet, transform.position, firePointNE.rotation);
            }
        }
    }

    #region Collision
    private bool collided = false;
    private void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.tag == "Wall"){       //Set it only if you want the AI to collide with walls
            collided = true;
        }
        collided = true; //Collides with anything
    }
    #endregion

    #region GetAngle
    /*private void GetAngle(){
        Vector2 direction = target.position - transform.position;
        distance = Vector2.Distance(target.position, transform.position);
        angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
    }*/
    #endregion

    #region WalkTimeCoroutine
    private bool addedDelay;
    private IEnumerator RandDelay(){
        addedDelay = true;
        int walkTime = Random.Range(1, 7);
        yield return new WaitForSeconds( walkTime );
        hasStopped = true;
        collided = false;
        timeLeft = Random.Range(1,3);
        addedDelay = false;
    }
    private bool bulletDelay;
    private IEnumerator BulletDelay(){
        bulletDelay = true;
        int a = 1;
        while(a < 2){
            Shoot();
            a++;
        }
        yield return new WaitForSeconds ( 1.0f );
        bulletDelay = false;
    }
    #endregion

    #region Comments
    /*
    *
    *   Last Edit Date : 29/10/2020
    *   Purpose : Creating the script
    *   Name : Paulescu Alexandru Mohamad
    *
    *   GLIXEL LAB 2020
    */
    #endregion

    /*if(hasStopped == true){                         //This "Faringa" script only uses 4 directions due to its body design
                desiredDirection = Random.Range(0, 4);
                if(desiredDirection == 0){
                    Offset = NW;
                    ar.SetBool("isNW", true);
                    ar.SetBool("isNE", false);
                    ar.SetBool("isSE", false);
                    ar.SetBool("isSW", false);
                }
                else if(desiredDirection == 1){
                    Offset = NE;
                    ar.SetBool("isNE", true);
                    ar.SetBool("isNW", false);
                    ar.SetBool("isSE", false);
                    ar.SetBool("isSW", false);
                }
                else if(desiredDirection == 2){
                    Offset = SE;
                    ar.SetBool("isSE", true);
                    ar.SetBool("isNE", false);
                    ar.SetBool("isSW", false);
                    ar.SetBool("isNW", false);
                }
                else if(desiredDirection == 3){
                    Offset = SW;
                    ar.SetBool("isSW", true);
                    ar.SetBool("isNE", false);
                    ar.SetBool("isNW", false);
                    ar.SetBool("isSE", false);
                }
            }
            if(timeLeft >= 0){              //Inbetween WalkTimes. Basically when the AI is not moving at all, we set the Speed ANIM value to 0
                ar.SetFloat("Speed", 0);
            }
            if(timeLeft < 0 && collided == false){               //During the WalkTime. Basically when the AI moves, we set the speed to the desired value ( 100 in our case )
                hasStopped = false;
                ar.SetFloat("Speed", 100);
                rb.MovePosition(rb.position + Offset * .75f * Time.deltaTime);   //We move the player 
            }
            if(collided == true){                           //If the AI collided
                if(desiredDirection == 0){
                    rb.MovePosition(rb.position + SE * 1f * Time.deltaTime);
                    ar.SetBool("isSE", true);
                    ar.SetBool("isNE", false);
                    ar.SetBool("isSW", false);
                    ar.SetBool("isNW", false);
                }
                else if(desiredDirection == 1){
                    rb.MovePosition(rb.position + SW * 1f * Time.deltaTime);
                    ar.SetBool("isSW", true);
                    ar.SetBool("isNE", false);
                    ar.SetBool("isNW", false);
                    ar.SetBool("isSE", false);
                }
                else if(desiredDirection == 2){
                    rb.MovePosition(rb.position + NW * 1f * Time.deltaTime);
                    ar.SetBool("isNW", true);
                    ar.SetBool("isNE", false);
                    ar.SetBool("isSE", false);
                    ar.SetBool("isSW", false);
                }
                else if(desiredDirection == 3){
                    rb.MovePosition(rb.position + NE * 1f * Time.deltaTime);
                    ar.SetBool("isNE", true);
                    ar.SetBool("isNW", false);
                    ar.SetBool("isSE", false);
                    ar.SetBool("isSW", false);
                }
            }
            if(timeLeft < 0 && !addedDelay)            //We call the waitDelay ( the Coroutine that has a random delay)
                StartCoroutine(RandDelay());
                */
    #region Notes
    /*
        The "timeLeft" variable acts as a timer, it takes a value (0, 1, 2 or 3 seconds) at start. Most AI's will start at different times, since the timeLeft values are
    random. We check if the AI is not moving and the we assign a random value to "desiredDirection", which will point to what direction the AI will go. We check if "timeLeft"
    is bigger than 0 ( basically the timer is still on ) and we set the "Speed" float in the Animator to 0 so that we can transition from MOVE to "Idle". 
        The next condition is that the timer has finished counting and the AI is moving ( we also the "Speed" float in Animator equal to a value (100 here) - IT IS NOT THE ACTUAL SPEED, its only 
    a number bigger than what we set in the Animator so we can transition. Also, the AI must not collide (collided == false) with anything as well.
        The next condition is for when the AI collides with anything in the scene. It checks what is the current direction ( desiredDirection ) and launches the AI in the
    opposite direction ( for ex., the current direction is North. This part will propell the AI to south, also setting it for the animation part).
        The last condition is for when the timer has finished as well. It calls the Coroutine, which in turn makes the AI move for a "walkTime" ammount of seconds, and afterwards
    it stops the AI(hasStopped), sets the collision to false(collided) and adds a new timer value. In order to FULLY understand the code, research "Use of Coroutines Unity"
    */
    #endregion
}