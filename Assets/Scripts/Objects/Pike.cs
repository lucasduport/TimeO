using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Pike : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.CompareTag("Player") && collider.GetComponent<PhotonView>().IsMine)
        {
            PlayerHealth ph = collider.GetComponent<PlayerHealth>();
            ph.HealthModifications(-ph.maxHealth);
        }
        
        if (collider.CompareTag("Enemy") && collider.GetComponent<PhotonView>().IsMine)
        {
            EnemyHealth eh = collider.GetComponent<EnemyHealth>();
            eh.HealthModifications(-eh.maxHealth);
        }

    }
}
