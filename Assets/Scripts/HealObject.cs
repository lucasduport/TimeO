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
        //booléen quidit si on se trouve sur le master (celui a qui appartient la room
        //là uniquement pour que la fonction ne soit jouée qu'une fois et non simultanément sur les deux ordis joueurs
        //car sinon ça fait spawn deux ennemis
        if (collider.transform.GetComponent<PhotonView>().IsMine && collider.transform.CompareTag("Player"))
        {
            PlayerHealth ph = collider.transform.GetComponent<PlayerHealth>();
            ph.HealthModifications(heal);
        }
        //destruction du consommable
        if (collider.transform.GetComponent<PhotonView>().IsMine) Destroy(gameObject);
    }
}
