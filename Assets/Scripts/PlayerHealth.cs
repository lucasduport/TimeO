using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
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
            Kill();
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

            //pas dépasser la valeur min et max
            if (currentHealth < 0) currentHealth = 0;
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

    public void Kill()
    {
        gameObject.GetComponent<Animator>().SetBool("killed",true);
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        
        if (PhotonNetwork.LocalPlayer.NickName == "Timéo")
        {
            //je reset le nickname pour pas que des anciens Timéo inactifs fassent passer que Timéo est dans la room
            PhotonNetwork.LocalPlayer.NickName = " ";
        }

        if (players.Length == 2)
        {
            GameObject theOne = players[0];
            GameObject theOther = players[1];

            if (theOne.name == gameObject.name)
            {
                CamManager.PlayerCam[theOther.name].gameObject.SetActive(true);
                CamManager.PlayerCam[theOne.name].gameObject.SetActive(false);
            }
            else
            {
                CamManager.PlayerCam[theOne.name].gameObject.SetActive(true);
                CamManager.PlayerCam[theOther.name].gameObject.SetActive(false);
            }

            CamManager.PlayerCam.Remove(gameObject.name);
        }
        else
        {
            CamManager.PlayerCam.Clear();
            PhotonNetwork.Destroy(gameObject);
        }
    }

    public IEnumerator HandleInvicibilityDelay()
    {
        yield return new WaitForSeconds(3f);
        isInvicible = false;
    }
}