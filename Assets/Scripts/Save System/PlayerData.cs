using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerData
{
    public int energy;

    public PlayerData(WeaponFIRE player){
        energy = player.energy;
    }
    
}
