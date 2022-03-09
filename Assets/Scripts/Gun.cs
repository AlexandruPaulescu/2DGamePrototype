using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    #region Variables
    [SerializeField]    private Movement movement;  //Ref to the Movement script of the Player
    [SerializeField]    private Rigidbody2D player;
    //-----------------------------------------------------------------------------------------
    private float distance;
    private float aimAssistOnDistance = 25f;
    private int enemyNumber;
    private float angle;
    //-----------------------------------------------------------------------------------------
    [HideInInspector]
    public GameObject[] enemies;
    Rigidbody2D[] enemiesRBS;
    Transform gun;      //Ref to the Transform component of the Player's gun
    SpriteRenderer spriteRenderer;
    #endregion

    #region MainScript
    private void Awake(){
        gun = GetComponent<Transform>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        #region CreateEnemyArray
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemiesRBS = new Rigidbody2D[enemies.Length];
        for(int i = 0; i < enemies.Length; i++){
            enemiesRBS[i] = enemies[i].GetComponent<Rigidbody2D>();
        }
        #endregion
    }
    private int numOfEnemies;
    private void FixedUpdate(){
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemiesRBS = new Rigidbody2D[enemies.Length];
        for(int i = 0; i < enemies.Length; i++){
            enemiesRBS[i] = enemies[i].GetComponent<Rigidbody2D>();
        }

        float aux = 1000;
        numOfEnemies = 0;
        for(int i = 0; i < enemies.Length; i++){
            distance = Vector2.Distance(player.position, enemiesRBS[i].position);
            if(enemies[i].activeSelf){
                if(distance < aimAssistOnDistance){
                    numOfEnemies++;
                }
            }
        }
        for(int i = 0; i < enemies.Length; i++){
            distance = Vector2.Distance(player.position, enemiesRBS[i].position);
            if(distance < aimAssistOnDistance){
                if(enemies[i].activeSelf){
                    if(distance < aux){
                        aux = distance;
                        enemyNumber = i;
                    }
                    Vector2 direction = (enemiesRBS[enemyNumber].position - player.position).normalized;
                    angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.Euler(0f, 0f, angle);
                    if(angle <= -90 || angle >= 90){
                        spriteRenderer.flipY = true;
                    }else spriteRenderer.flipY = false;
                }
            }
        }
        if(numOfEnemies == 0){
            RotateWeapon();
        }
    }

    #region RotateWeapon
    private float H;
    private float V;
    void RotateWeapon(){        //Part for rotating freely
        H = movement.hor;
        V = movement.ver;

        #region GunRotation
        if (H > 0){
            if (V >= 0 && V <= 0.1f)
            {
                gun.rotation = Quaternion.Euler(new Vector3(0, 0, 6));
            }
            if (V >= 0.1f && V <= 0.2f)
            {
                gun.rotation = Quaternion.Euler(new Vector3(0, 0, 10));
            }
            if (V >= 0.2f && V <= 0.3f)
            {
                gun.rotation = Quaternion.Euler(new Vector3(0, 0, 20));
            }
            if (V >= 0.3f && V <= 0.4f)
            {
                gun.rotation = Quaternion.Euler(new Vector3(0, 0, 30));
            }
            if (V >= 0.4f && V <= 0.5f)
            {
                gun.rotation = Quaternion.Euler(new Vector3(0, 0, 40));
            }
            if (V >= 0.5f && V <= 0.6f)
            {
                gun.rotation = Quaternion.Euler(new Vector3(0, 0, 50));
            }
            if (V >= 0.6f && V <= 0.7f)
            {
                gun.rotation = Quaternion.Euler(new Vector3(0, 0, 60));
            }
            if (V >= 0.7f && V <= 0.8f)
            {
                gun.rotation = Quaternion.Euler(new Vector3(0, 0, 70));
            }
            if (V >= 0.8f && V <= 0.9f)
            {
                gun.rotation = Quaternion.Euler(new Vector3(0, 0, 80));
            }
            if (V >= 0.9f && V <= 1)
            {
                gun.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
            }
            if (V < 0 && V >= -0.1f)
            {
                gun.rotation = Quaternion.Euler(new Vector3(0, 0, -6));
            }
            if (V <= -0.1f && V >= -0.2f)
            {
                gun.rotation = Quaternion.Euler(new Vector3(0, 0, -10));
            }
            if (V <= -0.2f && V >= -0.3f)
            {
                gun.rotation = Quaternion.Euler(new Vector3(0, 0, -20));
            }
            if (V <= -0.3f && V >= -0.4f)
            {
                gun.rotation = Quaternion.Euler(new Vector3(0, 0, -30));
            }
            if (V <= -0.4f && V >= -0.5f)
            {
                gun.rotation = Quaternion.Euler(new Vector3(0, 0, -40));
            }
            if (V <= -0.5f && V >= -0.6f)
            {
                gun.rotation = Quaternion.Euler(new Vector3(0, 0, -50));
            }
            if (V <= -0.6f && V >= -0.7f)
            {
                gun.rotation = Quaternion.Euler(new Vector3(0, 0, -60));
            }
            if (V <= -0.7f && V >= -0.8f)
            {
                gun.rotation = Quaternion.Euler(new Vector3(0, 0, -70));
            }
            if (V <= -0.8f && V >= -0.9f)
            {
                gun.rotation = Quaternion.Euler(new Vector3(0, 0, -80));
            }
            if (V <= -0.9 && V >= -1)
            {
                gun.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
            }
        }
        if (H < 0){
            if (V >= 0 && V <= 0.1f)
            {
                gun.rotation = Quaternion.Euler(new Vector3(180, 0, 180));
            }
            if (V >= 0.1f && V <= 0.2f)
            {
                gun.rotation = Quaternion.Euler(new Vector3(180, 0, 190));
            }
            if (V >= 0.2f && V <= 0.3f)
            {
                gun.rotation = Quaternion.Euler(new Vector3(180, 0, 200));
            }
            if (V >= 0.3f && V <= 0.4f)
            {
                gun.rotation = Quaternion.Euler(new Vector3(180, 0, 210));
            }
            if (V >= 0.4f && V <= 0.5f)
            {
                gun.rotation = Quaternion.Euler(new Vector3(180, 0, 220));
            }
            if (V >= 0.5f && V <= 0.6f)
            {
                gun.rotation = Quaternion.Euler(new Vector3(180, 0, 230));
            }
            if (V >= 0.6f && V <= 0.7f)
            {
                gun.rotation = Quaternion.Euler(new Vector3(180, 0, 240));
            }
            if (V >= 0.7f && V <= 0.8f)
            {
                gun.rotation = Quaternion.Euler(new Vector3(180, 0, 250));
            }
            if (V >= 0.8f && V <= 0.9f)
            {
                gun.rotation = Quaternion.Euler(new Vector3(180, 0, 260));
            }
            if (V >= 0.9f && V <= 1)
            {
                gun.rotation = Quaternion.Euler(new Vector3(180, 0, 270));
            }
            if(V == 1 ) gun.rotation = Quaternion.Euler(new Vector3(180, 0, 270));









            if (V < 0 && V >= -0.1f)
            {
                gun.rotation = Quaternion.Euler(new Vector3(180, 0, 180));
            }
            if (V <= -0.1f && V >= -0.2f)
            {
                gun.rotation = Quaternion.Euler(new Vector3(180, 0, 170));
            }
            if (V <= -0.2f && V >= -0.3f)
            {
                gun.rotation = Quaternion.Euler(new Vector3(180, 0, 160));
            }
            if (V <= -0.3f && V >= -0.4f)
            {
                gun.rotation = Quaternion.Euler(new Vector3(180, 0, 150));
            }
            if (V <= -0.4f && V >= -0.5f)
            {
                gun.rotation = Quaternion.Euler(new Vector3(180, 0, 140));
            }
            if (V <= -0.5f && V >= -0.6f)
            {
                gun.rotation = Quaternion.Euler(new Vector3(180, 0, 130));
            }
            if (V <= -0.6f && V >= -0.7f)
            {
                gun.rotation = Quaternion.Euler(new Vector3(180, 0, 120));
            }
            if (V <= -0.7f && V >= -0.8f)
            {
                gun.rotation = Quaternion.Euler(new Vector3(180, 0, 110));
            }
            if (V <= -0.8f && V >= -0.9f)
            {
                gun.rotation = Quaternion.Euler(new Vector3(180, 0, 100));
            }
            if (V <= -0.9 && V >= -1)
            {
                gun.rotation = Quaternion.Euler(new Vector3(180, 0, 90));
            }
            if(V == -1) gun.rotation = Quaternion.Euler(new Vector3(180, 0, 90));
        }
        /*if(H == 0 && V == 0){                                                       //ONLY ENABLE IF YOU WANT TO RESET WEAPON ROTATION AFTER "NO TOUCH" ON THE JOYSTICK
            gun.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }*/
        #endregion
    }
    #endregion

    #region ToggleAimButton
    private bool isOn = false;
    private bool isOff = true;
    private int nrOfPresses = 0;
    public void ToggleAim(){
        nrOfPresses++;
        if(nrOfPresses%2==1){
            isOn = true;
            isOff = false;
        }
        else{
            isOn = false;
            isOff = true;
            H = movement.hor;
            V = movement.ver;
            if(H == 0 && V == 0){
                if(angle < -90 || angle > 90){
                    spriteRenderer.flipY = false;
                    gun.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                }
                else{
                    gun.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                }       
            }
            //It toggles the AimAssist OFF, while also centering the gun rotation for once, before the player moves the JoyStick again
        }
    }
    #endregion

    #endregion

    #region Comments
    /*
    *
    *   Last Edit Date : 4/10/2020
    *   Purpose : Arranging the script
    *   Name : Paulescu Alexandru Mohamad
    *
    *   Last Edit Date : 20/10/2020 - 15PM
    *   Purpose : Adding comments to the script
    *   Name : Paulescu Alexandru Mohamad
    *
    *   Last Edit Date : 11/1/2020 - 14PM
    *   Purpose : Succesfully implemented the Aim Assist
    *   Name : Paulescu Alexandru Mohamad
    *
    *   GLIXEL LAB 2020
    */
    #endregion
}
