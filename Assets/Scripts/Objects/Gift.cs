using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Gift : MonoBehaviour
{
   
    void OnTriggerEnter2D(Collider2D collider)
    {
        int h = Random.Range(0, 2); //nb aléatoire pour savoir s'il gagne ou perde de la vie

        if (collider.transform.GetComponent<PhotonView>().IsMine && collider.transform.CompareTag("Player"))
        {
            PlayerHealth ph = collider.transform.GetComponent<PlayerHealth>();
            if(h==0)
            {
                ph.HealthModifications(30);
            }
            else
            {
                ph.HealthModifications(-30);
            }
            //destruction du consommable seulement chez le joueur qui l'a pris
            Destroy(gameObject);
        }

    }
}
