using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeGun : MonoBehaviour
{
    #region Variables 
    public Button button;                   //Ref to button
    public Sprite[] mainGun;                //Creating a Sprite to store

    public SpriteRenderer GunRend;          //Ref to the renderer of the player ( IMP to mention, this will always be the Player's gun) 

    private SpriteRenderer pickupRend;       //Ref to the renderer of what remains on the ground

    private ChangeGunParent PlayerGunAUX;           //Ref to the Parent script to acces the "PlayerGun" variable
    private GameObject SHOTGUN; 
    
    #endregion

    #region MainScript
    private void Start(){
        //Here we find the Object that contains the Parent script
        SHOTGUN = GameObject.FindWithTag("UpdateState");
        if(SHOTGUN != null){
            PlayerGunAUX = SHOTGUN.GetComponent<ChangeGunParent>();
        }

        pickupRend = GetComponent<SpriteRenderer>();    //We set the pickupRend to the SpriteRenderer of the current weapon

        PlayerGunAUX.PlayerGun = GunRend.sprite;     //Assigning the sprite of the Player's Gun

        button.gameObject.SetActive(false); 
    }

    private void OnTriggerEnter2D(Collider2D collider){     //Check if the player collides with the gun and set it to Visible
        if(collider.CompareTag("Player"))
            button.gameObject.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collider){      //Check if the player stops colliding with the gun and set it to Invisible
        button.gameObject.SetActive(false);
    }

    public void Change(){                           //The function the PickUp button calls when pressed
        GunRend.sprite = pickupRend.sprite;     
        pickupRend.sprite = PlayerGunAUX.PlayerGun;
        PlayerGunAUX.PlayerGun = GunRend.sprite;
    }
    
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
