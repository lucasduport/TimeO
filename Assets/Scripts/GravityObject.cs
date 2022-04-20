using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Threading;
public class GravityObject : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool GravityEnabled = false;
    

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.GetComponent<PhotonView>().IsMine && collider.transform.CompareTag("Player"))
        {
            GravityEnabled = true;
            Destroy(gameObject);
        }

    }



}
