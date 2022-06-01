using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HitEnemies : MonoBehaviour
{
    public int DamageParCoup = 20;
    public Transform t;
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            float val = 999999;
            GameObject[] a = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject go in a)
            {
                if (Math.Abs(go.transform.position.x) < Math.Abs(val))
                {
                    val = go.transform.position.x;
                }
            }
            
            if ((val > t.position.x && t.localScale.x >0) || (val < t.position.x && t.localScale.x < 0))
            {
                t.localScale = new Vector3(-t.localScale.x, t.localScale.y, t.localScale.z);
            }


            PlayerHealth playerHealth = collider.GetComponent<PlayerHealth>();
            Animator anim = transform.parent.GetComponent<Animator>();
            anim.Play("hit");
            playerHealth.HealthModifications(-DamageParCoup);
        }
        if (collider.CompareTag("Enemy"))
        {
            t.localScale = new Vector3(-t.localScale.x, t.localScale.y, t.localScale.z);
        }
    }

}
