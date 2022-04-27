using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEnemies : MonoBehaviour
{
    public int DamageParCoup = 20;
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collider.GetComponent<PlayerHealth>();
            Animator anim = transform.parent.GetComponent<Animator>();
            anim.Play("hit");
            playerHealth.HealthModifications(-DamageParCoup);
        }
    }

}
