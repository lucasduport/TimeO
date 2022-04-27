using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Photon.Pun;
public class WeaponBranch : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            PhotonView pView = collider.GetComponent<PhotonView>();
            if (pView.IsMine)
            {
                Animator ph = collider.GetComponent<Animator>();
                ph.SetBool("isBranch", true);
                StartCoroutine(Destruction());
            }
            foreach (var c in GetComponents<Collider2D>())
            {
                c.enabled = false;
            }
            transform.localScale = Vector3.zero;
            
        }
    }
    
    IEnumerator Destruction()
    {
        yield return new WaitForSeconds(0.5f);
        if (GetComponent<PhotonView>().IsMine)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }

}
