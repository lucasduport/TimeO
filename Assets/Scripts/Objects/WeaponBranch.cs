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
        if (collider.transform.CompareTag("Player"))
        {
            if (collider.transform.GetComponent<PhotonView>().IsMine)
            {
                Animator ph = collider.transform.GetComponent<Animator>();
                ph.SetBool("isBranch", true);
            }
            gameObject.transform.localScale = Vector3.zero;
            foreach (var c in gameObject.GetComponents<Collider2D>())
            {
                c.enabled = false;
            }
            StartCoroutine(Destruction());
        }
    }

    IEnumerator Destruction()
    {
        yield return new WaitForSeconds(0.5f);
        if (gameObject.GetComponent<PhotonView>().IsMine &&
            gameObject.transform.localScale == Vector3.zero)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
