using System;
using System.Collections;
using Photon.Pun;
using UnityEngine;
using UnityEngine.XR;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    
    public HealthBar healthBar;
    private Animator Anim;
    private Transform ht;

    public Collider2D[] Colliders;
    public String prefabToInstantianteOnDeath = "";
    void Start()
    {
        currentHealth = maxHealth;
        Anim = GetComponent<Animator>();
        Anim.SetInteger("health",maxHealth);
    }

    void Update()
    {
        if (!GetComponent<PhotonView>().IsMine) return;
        if (transform.localScale.x < 0 && healthBar.transform.localScale.x > 0)
        {
            healthBar.transform.localScale = new Vector3(-healthBar.transform.localScale.x,healthBar.transform.localScale.y,healthBar.transform.localScale.z);
        }
        if (gameObject.transform.localScale.x > 0 && healthBar.transform.localScale.x < 0)
        {
            healthBar.transform.localScale = new Vector3(-healthBar.transform.localScale.x,healthBar.transform.localScale.y,healthBar.transform.localScale.z);
        }
        
        healthBar.SetHealth(currentHealth);
    }

    public void HealthModifications(int number)
    {
        currentHealth += number;
        Anim.SetInteger("health",currentHealth);
        if (currentHealth <= 0 && GetComponent<PhotonView>().IsMine)
        {
            if (prefabToInstantianteOnDeath != "")
            {
                PhotonNetwork.Instantiate(prefabToInstantianteOnDeath, transform.position, Quaternion.identity);
            }
            foreach (var c in Colliders)
            {
                c.enabled = true;
            }
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
