using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class HitPlayers : MonoBehaviour
{
    public int DamageParCoup = 30;
    void OnTriggerStay2D(Collider2D collider)
    {
        if (GetComponent<PhotonView>().IsMine && collider.transform.CompareTag("Enemy"))
        {
            EnemyHealth eh = collider.transform.GetComponent<EnemyHealth>();
            eh.HealthModifications(-DamageParCoup);
        }
    }
}
