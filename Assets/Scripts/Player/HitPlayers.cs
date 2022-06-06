using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class HitPlayers : MonoBehaviour
{
    public int DamageParCoup = 30;
    public AudioClip sound;

    private void OnEnable()
    {
        Menu.instance.PlayClipAt(sound, transform.position);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.CompareTag("Enemy") && collider.GetComponent<PhotonView>().IsMine)
        {
            EnemyHealth eh = collider.transform.GetComponent<EnemyHealth>();
            eh.HealthModifications(-DamageParCoup);
        }
    }
}
