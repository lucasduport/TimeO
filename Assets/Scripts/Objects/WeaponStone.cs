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
    private void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(5, 0);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            if (collider.GetComponent<PhotonView>().IsMine && !collider.GetComponent<Animator>().GetBool("isStone"))
            {
                Animator ph = collider.GetComponent<Animator>();
                ph.SetBool("isStone", true);
                StartCoroutine(Destruction());
            }
            foreach (var c in gameObject.GetComponents<Collider2D>())
            {
                c.enabled = false;
            }
            gameObject.transform.localScale = Vector3.zero;
            
        }

        if (collider.CompareTag("Stone"))
        {
            if (collider.GetComponent<PhotonView>().IsMine &&
                Math.Abs(collider.GetComponent<Rigidbody2D>().velocity.y) == 0)
            {
                PhotonNetwork.Instantiate("Fire", gameObject.transform.position, Quaternion.identity);
                StartCoroutine(Destruction());
            }
            gameObject.transform.localScale = Vector3.zero;
        }
    }

    IEnumerator Destruction()
    {
        yield return new WaitForSeconds(1f);
        if (gameObject.GetComponent<PhotonView>().IsMine &&
            gameObject.transform.localScale == Vector3.zero)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }
}