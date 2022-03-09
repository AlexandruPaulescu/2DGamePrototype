using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //GameObject[] enemies;

    [SerializeField]    private GameObject gun;
    [SerializeField]    private GameObject sword;
    [SerializeField]    private GameObject gun_button;
    [SerializeField]    private GameObject sword_button;

    private void Start(){    
        if(gun.activeSelf){
            gun_button.SetActive(true);
            sword_button.SetActive(false);
        }
        else if(sword.activeSelf){
            gun_button.SetActive(false);
            sword_button.SetActive(true);
        }
    }
}
