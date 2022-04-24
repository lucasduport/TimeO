using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public int Damages = 50;
    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.transform.CompareTag("Player"))
        {
            if (collider.transform.GetComponent<PhotonView>().IsMine)
            {
                PlayerHealth ph = collider.transform.GetComponent<PlayerHealth>();
                ph.HealthModifications(-Damages);
            }
        }

        if (collider.transform.CompareTag("Enemy"))
        {
            if (collider.transform.GetComponent<PhotonView>().IsMine)
            {
                EnemyHealth ph = collider.transform.GetComponent<EnemyHealth>();
                ph.HealthModifications(-Damages);
            }
        }
    }
}
