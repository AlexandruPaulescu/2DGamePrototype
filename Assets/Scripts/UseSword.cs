using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseSword : MonoBehaviour
{
    [SerializeField]    private SetSliderHealth healthBar;
    [SerializeField]    private WeaponFIRE WF;     
    Animation animation;
    private void Start(){
        animation = GetComponent<Animation>();
        healthBar.SetMaxHealth(100f); //Player's MAX health
    }

    private void Update(){
        healthBar.SetHealth(PlayerPrefs.GetFloat("health"));        //Setting the Saved HEALTH
    }

    public void Shoot(){
        animation.Play("Attack");
    }
}
