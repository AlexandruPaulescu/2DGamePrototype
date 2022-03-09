using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class WeaponFIRE : MonoBehaviour
{
    #region Variables

    #region Bullets
    [SerializeField] private GameObject pistolBullet;      //Ref to the pistolBullet PREFAB

    [SerializeField] private GameObject sniperBullet;      //Ref to the sniperBullet PREFAB

    [SerializeField] private GameObject shotgunBullet;      //Ref to the shotgunBullet PREFAB

    [SerializeField] private GameObject smgBullet;      //Ref to the smgBullet PREFAB
    #endregion
        

    #region FirePoints
    [SerializeField] private Transform Pistol_firePoint;    //Ref to the Transform component of the firePoint ( find it the scene, as a child of the gun )

    [SerializeField] private Transform Sniper_firePoint;

    [SerializeField] private Transform Shotgun1_firePoint;

    [SerializeField] private Transform Shotgun2_firePoint;

    [SerializeField] private Transform Shotgun3_firePoint;

    [SerializeField] private Transform SMG_firePoint;
    #endregion


    [SerializeField] private Text myText;                   //Displays Energy
    [SerializeField] private GunDelay delays;               //Ref to the gun delays script
    [SerializeField] private Enemy enemy;                   //Ref to the enemy script
    [SerializeField] private SetSliderHealth healthBar;
    [SerializeField] private SetSliderEnergy energyBar;

    [HideInInspector] public int energy;
    
    private bool isEmpty = false;           //Check to see if we still have mana
    private int currentGun;
    private int buildIndex;
    private float LastPressTime;       
    private Animation gunAnimation;
    private Scene currentScene;

    #endregion 

    #region MainScript
    void Start(){
        /*
         * First, the scene Loads any previous save state with the "Load()". Then It automatically saves, for IDK, making sure it will.
         * It is Awake() instead of Start(), beacause I wanna Load() before anything happens in the scene. It can work with Start but I Believe I will run over bugs    
         */
        gunAnimation = GetComponent<Animation>();

        currentScene = SceneManager.GetActiveScene ();
        buildIndex = currentScene.buildIndex; 

        healthBar.SetMaxHealth(100f); //Player's MAX health
        energyBar.SetMaxEnergy(200);

        Load();
        Save();
    }
    void Update(){
        //Every ms, the scene saves and updates the UI Text.
        Save();
        if(buildIndex != 0){
            //We check if the active scene is NOT the lobby so we dont get any errors
            myText.text = energy.ToString();
        }
        currentGun = PlayerPrefs.GetInt("final_x");

        #region GiveDamageOnZeroEnergy
        if(energy <= 0){
            isEmpty = true;
        }
        else{
            isEmpty = false;
        }
        if(isEmpty && !addedDelay){
            StartCoroutine(DamageDelay());
        }
        #endregion
        energyBar.SetEnergy(energy);                                //Setting the Saved ENERGY
        healthBar.SetHealth(PlayerPrefs.GetFloat("health"));        //Setting the Saved HEALTH
    }   
    public void ResetEnergy(){
        //For testing purposes only
        energy = 200;
        isEmpty = false;
        Save();
    }
    public void Shoot(){      
        //-------------------------------------------------
        if(LastPressTime + delays.gunDelay > Time.unscaledTime)  
            return;                                             //This piece of code checks to see if the Button delay is true, else returns out of the function
        LastPressTime = Time.unscaledTime;                      //It updates for each gun using "GameManager" Script
        //-------------------------------------------------
        Save();
        Load();
        //It is called from the "Shoot" UI Button in the scene
        /*
         * Basically when its called, it takes "bulletPrefab.manaCost" ammount from the player ( in our case its 3 )
         * Then, we check if the mana is available, if it is we start shooting
         * If its lower than 0, we make sure we cant shoot anymore, save the current mana, but also set the "mana" variable to be 0, in order to not drop below a positive ammount
         */
        TakeEnergy();
        if(currentGun == 14){    //If the active gun is the PISTOL
            if (energy > 0 && isEmpty == false){
            Instantiate(pistolBullet, Pistol_firePoint.position, Pistol_firePoint.rotation);
            isEmpty = false;
            }
            else if(energy < 0){
            energy = 0;
            isEmpty = true;
            Save();
            }
        }
        else if(currentGun == 20){    //If the active gun is the SNIPER
            if (energy > 0 && isEmpty == false){
            Instantiate(sniperBullet, Sniper_firePoint.position, Sniper_firePoint.rotation);
            isEmpty = false;
            }
            else if(energy < 0){
            energy = 0;
            isEmpty = true;
            Save();
            }
        }
        else if(currentGun == 23){    //If the active gun is the SHOTGUN
            if (energy > 0 && isEmpty == false){

            gunAnimation.Play("shotgunRecoil");
            Shake.Instance.ShakeCamera(7f, 2f, .15f);   
            GameObject go1 = Instantiate(shotgunBullet, Shotgun1_firePoint.position, Shotgun1_firePoint.rotation);
            GameObject go2 = Instantiate(shotgunBullet, Shotgun2_firePoint.position, Shotgun2_firePoint.rotation);
            GameObject go3 = Instantiate(shotgunBullet, Shotgun3_firePoint.position, Shotgun3_firePoint.rotation);
            go1.transform.localScale = new Vector3(2.5f, 2.5f, 0f);
            go2.transform.localScale = new Vector3(2.5f, 2.5f, 0f);
            go3.transform.localScale = new Vector3(2.5f, 2.5f, 0f);

            isEmpty = false;
            }
            else if(energy < 0){
            energy = 0;
            isEmpty = true;
            Save();
            }
        }
        else if(currentGun == 27){    //If the active gun is the SMG
            if (energy > 0 && isEmpty == false){
            Instantiate(smgBullet, SMG_firePoint.position, SMG_firePoint.rotation);
            isEmpty = false;
            }
            else if(energy < 0){
            energy = 0;
            isEmpty = true;
            Save();
            }
        }
        
    }
    void TakeEnergy(){
        /*
         * We check if we have mana with our "isEmpty" bool. If we do, we eliminate mana and Save the ammount. Otherwise, we will add some in the future, depending on what we wanna do
         */
        if (isEmpty == false && currentGun == 14)   //PISTOL
        {
            energy -= 1;
            Save();
        }
        else if (isEmpty == false && currentGun == 20)  //SNIPER
        {
            energy -= 10;
            Save();
        }
        else if (isEmpty == false && currentGun == 23)  //SHOTGUN
        {
            energy -= 4;
            Save();
        }
        else if (isEmpty == false && currentGun == 27)  //SMG
        {
            energy -= 3;
            Save();
        }
    }

    #region FunctionGiveDamageOnZeroEnergy
    private bool addedDelay;
    IEnumerator DamageDelay(){
        addedDelay = true;
        yield return new WaitForSeconds ( 3.0f );
        if(isEmpty)
            enemy.TakeDamage(20f);
        addedDelay = false;
    }
    #endregion
    void Save(){
        SaveSystem.SavePlayer(this);
    }
    void Load(){
        PlayerData data = SaveSystem.LoadPlayer();
        energy = data.energy;
        if(energy > 200){
            energy = 200;
        }
    }
    
    #endregion

    #region Comments
    /*
    *
    *   Last Edit Date : 8/11/2020 - 11AM
    *   Purpose : Added Energy + Health bar + Save System
    *   Name : Paulescu Alexandru Mohamad
    *
    *   GLIXEL LAB 2020
    */
    #endregion
}