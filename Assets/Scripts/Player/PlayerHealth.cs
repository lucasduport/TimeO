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

    void Kill()
    {
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
                POVManager.PlayerCam[theOther.name].gameObject.SetActive(true);
                POVManager.PlayerCam[theOne.name].gameObject.SetActive(false);
            }
            else
            {
                POVManager.PlayerCam[theOne.name].gameObject.SetActive(true);
                POVManager.PlayerCam[theOther.name].gameObject.SetActive(false);
            }
            GameObject.Find("SpectateText").GetComponent<Text>().color = new Color(1f,1f,1f,1f);
            POVManager.PlayerCam.Remove(gameObject.name);
        }
        else
        {
            POVManager.PlayerCam.Clear();
            PhotonNetwork.Destroy(gameObject);
        }
    }
    public IEnumerator HandleInvicibilityDelay()
    {
        yield return new WaitForSeconds(2.5f);
        isInvicible = false;
    }
}