using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetSliderEnergy : MonoBehaviour
{
    [SerializeField]    private Slider slider;

    public void SetMaxEnergy(int energy){
        slider.maxValue = energy;
        slider.value = energy;
    }

    public void SetEnergy(int energy){
        slider.value = energy;
    }
}
