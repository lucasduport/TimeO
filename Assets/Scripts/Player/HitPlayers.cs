using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class HitPlayers : MonoBehaviour
{
    public int DamageParCoup = 30;
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.CompareTag("Enemy"))
        {
            EnemyHealth eh = collider.transform.GetComponent<EnemyHealth>();
            eh.HealthModifications(-DamageParCoup);
        }
    }
}
