using System.Collections;
using UnityEngine;
using UnityEngine.XR;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public SpriteRenderer spriteRenderer;
    public HealthBar healthBar;

    private bool isInvicible = false;
    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        healthBar.SetHealth(currentHealth);
    }

    public void HealthModifications(int number)
    {                          //j'ai rajouté ce ou pour pouvoir se heal même quand on est invincible
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

    public IEnumerator HandleInvicibilityDelay()
    {
        yield return new WaitForSeconds(3f);
        isInvicible = false;
    }
}
