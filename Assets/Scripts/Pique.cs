using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Pique : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.GetComponent<PhotonView>().IsMine && collider.transform.CompareTag("Player"))
        {
            PlayerHealth ph = collider.transform.GetComponent<PlayerHealth>();
            ph.HealthModifications(-100);
            //destruction du consommable seulement chez le joueur qui l'a pris
        }

    }
}
