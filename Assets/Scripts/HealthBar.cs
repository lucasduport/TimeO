using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider slider;

    //init de la barre de vie
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    //cette méthode sera appellée lors de la régen ou d'un hit
    public void SetHealth(int number)
    {
        slider.value = number;
    }
}
