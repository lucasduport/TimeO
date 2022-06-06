using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Threading;
public class GravityObject : MonoBehaviour
{
    public AudioClip sound;
    // Start is called before the first frame update
    
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            Menu.instance.PlayClipAt(sound, transform.position);
            POVManager.GravityEnabled = true;
            if (GetComponent<PhotonView>().IsMine) PhotonNetwork.Destroy(gameObject);
        }

    }



}
