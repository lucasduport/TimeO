using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class HealObject : MonoBehaviour
{
    // Start is called before the first frame update
    public int heal;
    void OnTriggerEnter2D(Collider2D collider)
    {
        if ( collider.transform.CompareTag("Player"))
        {
            if (collider.transform.GetComponent<PhotonView>().IsMine)
            {
                PlayerHealth ph = collider.transform.GetComponent<PlayerHealth>();
                ph.HealthModifications(heal);
            }
            //destruction du consommable seulement chez le joueur qui l'a pris
            Destroy(gameObject);
        }
    }
}
