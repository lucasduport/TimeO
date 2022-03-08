using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobDamages : MonoBehaviour
{
    public int damages = 20;
    // Start is called before the first frame update

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.transform.GetComponent<PlayerHealth>();
            playerHealth.HealthModifications(-damages);
        }
    }

}
