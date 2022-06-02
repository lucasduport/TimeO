using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Threading;

public class InvicibilityHat : MonoBehaviour
{
    // Start is called before the first frame update
    public int time;
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            if (collider.GetComponent<PhotonView>().IsMine)
            {
                PlayerHealth ph = collider.transform.GetComponent<PlayerHealth>();
                ph.Invincibil(time);
            }

            if (GetComponent<PhotonView>().IsMine) PhotonNetwork.Destroy(gameObject);
        }
    }
}
