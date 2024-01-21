using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthSlider : MonoBehaviour
{

    public Slider health_slider;
    
    public void setMaxHealth(float health){
        
        health_slider.maxValue = health;
        health_slider.value = health;
    }
    public void setHealth(float health){
        health_slider.value = health;
    }

    public float getHealth(){
        return health_slider.value;
    }
    public float GetMaxHealth(){
        return health_slider.maxValue;
    }
}
