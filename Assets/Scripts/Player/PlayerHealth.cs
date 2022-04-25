using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    

    public SpriteRenderer spriteRenderer;
    public HealthBar healthBar;

    private bool isInvicible = false;
    public bool Killed = false;

    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        healthBar.SetHealth(currentHealth);
        if (!Killed && currentHealth <= 0)
        {
            gameObject.GetComponent<Animator>().SetBool("killed",true);
            Killed = true;
        }
    }

    public void HealthModifications(int number)
    {
        //j'ai rajouté ce ou pour pouvoir se heal même quand on est invincible
        if (!isInvicible || (isInvicible && number > 0))
        {
            //le joueur va subir des dégâts
            if (number < 0)
            {
                isInvicible = true;
                StartCoroutine(InvicibilityFlash());
                StartCoroutine(HandleInvicibilityDelay());
            }

            currentHealth += number;

            //pas dépasser la valeur max
            if (currentHealth > maxHealth) currentHealth = maxHealth;

            healthBar.SetHealth(currentHealth);
        }
    }

    public IEnumerator InvicibilityFlash()
    {
        while (isInvicible)
        {
            spriteRenderer.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(0.15f);
            spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(0.15f);
        }
    }
    
    public IEnumerator HandleInvicibilityDelay()
    {
        yield return new WaitForSeconds(2.5f);
        isInvicible = false;
    }
}