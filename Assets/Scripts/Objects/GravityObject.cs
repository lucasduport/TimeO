using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Threading;
public class GravityObject : MonoBehaviour
{
    // Start is called before the first frame update
    
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            POVManager.GravityEnabled = true;
            if (GetComponent<PhotonView>().IsMine) PhotonNetwork.Destroy(gameObject);
        }

    }



}
