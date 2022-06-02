using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    public Transform[] wayPoints;
    public int destPoint = 0;
    private Transform target;

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
    void Update()
    {
        target = wayPoints[destPoint];
        transform.position = Vector3.MoveTowards(transform.position, target.position, 0.002f);
        if (transform.position == target.position)
        {
            destPoint = (destPoint + 1) % 2;
        }
    }
}
