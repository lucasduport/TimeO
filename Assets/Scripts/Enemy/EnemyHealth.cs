using System.Collections;
using Photon.Pun;
using UnityEngine;
using UnityEngine.XR;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public static int currentHealth;
    
    public HealthBar healthBar;
    private Transform ht;
    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (gameObject.transform.localScale.x < 0 && healthBar.transform.localScale.x > 0)
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
        if (currentHealth < 0)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
