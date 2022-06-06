using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Pike : MonoBehaviour
{
    public AudioClip sound;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Menu.instance.PlayClipAt(sound, transform.position);
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            if (collider.GetComponent<PhotonView>().IsMine)
            {
                PlayerHealth ph = collider.GetComponent<PlayerHealth>();
                ph.HealthModifications(-ph.maxHealth);
            }
        }
        
        if (collider.CompareTag("Enemy") && collider.GetComponent<PhotonView>().IsMine)
        {
            EnemyHealth eh = collider.GetComponent<EnemyHealth>();
            eh.HealthModifications(-eh.maxHealth);
        }

    }
}
