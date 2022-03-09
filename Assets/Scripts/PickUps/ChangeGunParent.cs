using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeGunParent : MonoBehaviour
{
    #region Variables 
    public Button button;                   //Ref to button
    public Sprite[] shotGun;                //Creating a Sprite to store
    public SpriteRenderer GunRend;          //Ref to the renderer of the player ( IMP to mention, this will always be the Player's gun) 
    private SpriteRenderer pickupRend;       //Ref to the renderer of what remains on the ground

    [HideInInspector]
    public Sprite PlayerGun;               //Creating a sprite that stores what the player has in his hand
    #endregion

    #region MainScript

    private void Awake(){
        pickupRend = GetComponent<SpriteRenderer>();    //We set the pickupRend to the SpriteRenderer of the current weapon

        PlayerGun = GunRend.sprite;     //Assigning the sprite of the Player's Gun 

        button.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collider){     //Check if the player collides with the gun and set it to Visible
        if(collider.CompareTag("Player"))
            button.gameObject.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collider){      //Check if the player stops colliding with the gun and set it to Invisible
        button.gameObject.SetActive(false);
    }

    public void shotgun(){                           //The function the PickUp button calls when pressed
        GunRend.sprite = pickupRend.sprite;     
        pickupRend.sprite = PlayerGun;
        PlayerGun = GunRend.sprite;
        
    }

    /*void Save(){
        SaveSystem.SaveWeapon(this);
    }
    void Load(){
        WeaponData data = SaveSystem.LoadWeapon();
        PlayerGun = data.PlayerGun;
    }*/

    #endregion

    #region Comments
    /*
    *
    *   Last Edit Date : 20/10/2020 - 5PM
    *   Purpose : Creating the script
    *   Name : Paulescu Alexandru Mohamad
    *
    *   GLIXEL LAB 2020
    */
    #endregion
}