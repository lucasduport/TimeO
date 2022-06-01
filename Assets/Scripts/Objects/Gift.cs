using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Gift : MonoBehaviour
{
   
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            if (collider.GetComponent<PhotonView>().IsMine)
            {
                PlayerHealth ph = collider.GetComponent<PlayerHealth>();
                int h = Random.Range(0, 2); //nb aléatoire pour savoir s'il gagne ou perde de la vie
                ph.HealthModifications( h==0? 30 : -30);
                GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 0f);
            }
            if (GetComponent<PhotonView>().IsMine)
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }
}
