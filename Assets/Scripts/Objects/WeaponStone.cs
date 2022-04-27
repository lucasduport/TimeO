using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Pun.Demo.Cockpit;
using Photon.Realtime;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class WeaponStone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            if (collider.GetComponent<PhotonView>().IsMine)
            {
                Animator ph = collider.GetComponent<Animator>();
                ph.SetBool("isStone", true);
                StartCoroutine(Destruction());
            }
            foreach (var c in GetComponents<Collider2D>())
            {
                c.enabled = false;
            }
            transform.localScale = Vector3.zero;
        }

        if (collider.CompareTag("Stone"))
        {
            if (collider.GetComponent<PhotonView>().IsMine && collider.GetComponent<Rigidbody2D>().velocity.y == 0)
            {
                PhotonNetwork.Instantiate("Fire", transform.position, Quaternion.identity);
                StartCoroutine(Destruction());
            }
            foreach (var c in gameObject.GetComponents<Collider2D>())
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