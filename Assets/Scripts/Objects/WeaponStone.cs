using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Pun.Demo.Cockpit;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class WeaponStone : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.CompareTag("Player") && !collider.transform.GetComponent<Animator>().GetBool("isStone"))
        {
            if (collider.transform.GetComponent<PhotonView>().IsMine)
            {
                Animator ph = collider.transform.GetComponent<Animator>();
                ph.SetBool("isStone", true);
                PhotonNetwork.Destroy(gameObject);
            }
        }
        
        if (collider.transform.CompareTag("Stone"))
        {
            if (collider.transform.GetComponent<PhotonView>().IsMine && Math.Abs(collider.transform.GetComponent<Rigidbody2D>().velocity.y) > 0.5 )
            {
                PhotonNetwork.Instantiate("Fire", gameObject.transform.position + new Vector3(-0.5f,2f,0f), Quaternion.identity);
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }


}
