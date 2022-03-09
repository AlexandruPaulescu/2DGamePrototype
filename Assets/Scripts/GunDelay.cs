using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunDelay : MonoBehaviour
{
    #region Variables
    private Sprite[] guns;                  //Ref to all the sprites that are part of the "Guns" tileset
    private SpriteRenderer gunRend;         //Ref to the sprite renderer component from the Player's gun

    [HideInInspector]
    public float gunDelay;

    private int initial_x;
    #endregion

    #region MainScript
    void Start()
    {
        guns = Resources.LoadAll<Sprite>("guns");       //Finding all the sprites in the "Guns" tileset
        gunRend = GetComponent<SpriteRenderer>();       //Accesing the ref to the sprite renderer component from the Player's gun

        initial_x = 14;
        if(PlayerPrefs.GetInt("final_x") == 0) gunRend.sprite = guns[14];
        else{
            if(PlayerPrefs.GetInt("final_x") == initial_x){
                PlayerPrefs.SetInt("final_x", 14);
                gunRend.sprite = guns[14];
            }
            else{
                gunRend.sprite = guns[PlayerPrefs.GetInt("final_x")];
            }
        }
    }

    void Update(){
        //Whenever you add a new gun to the game, after you add the buttons and the tags as well, here you must add a new if, where you replace x in guns[x] with the sprite
        //that you want to use ( for us, 14 is the pistol, 20 is the sniper etc. ) and also add a gunDelay equal to the delay you want the weapon to have between shots

        if(gunRend.sprite == guns[14]){ //Pistol
            gunDelay = 0.5f;
            PlayerPrefs.SetInt("final_x", 14);
        }
        if(gunRend.sprite == guns[20]){ //Sniper
            gunDelay = 3f;
            PlayerPrefs.SetInt("final_x", 20);
        }
        if(gunRend.sprite == guns[23]){ //Shotgun
            gunDelay = 0f;
            PlayerPrefs.SetInt("final_x", 23);
        }
        if(gunRend.sprite == guns[27]){ //SMG
            gunDelay = 0.1f;
            PlayerPrefs.SetInt("final_x", 27);
        }
    }
    #endregion

    #region Comments
    /*
    *
    *   Last Edit Date : 23/10/2020 - 11AM
    *   Purpose : Created the script from scratch
    *   Name : Paulescu Alexandru Mohamad
    *
    *   GLIXEL LAB 2020
    */
    #endregion
}