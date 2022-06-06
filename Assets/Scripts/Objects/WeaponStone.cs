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
    public AudioClip sound;
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player") && !collider.GetComponent<Animator>().GetBool("isStone"))
        {
            Menu.instance.PlayClipAt(sound, transform.position);
            if (collider.GetComponent<PhotonView>().IsMine)
            {
                Animator ph = collider.GetComponent<Animator>();
                StartCoroutine(Anim(ph));
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
            if (collider.GetComponent<PhotonView>().IsMine)
            {
                if (GetComponent<Rigidbody2D>().velocity.y == 0) PhotonNetwork.Instantiate("Fire", transform.position, Quaternion.identity);
                StartCoroutine(Destruction());
            }
            foreach (var c in gameObject.GetComponents<Collider2D>())
            {
                c.enabled = false;
            }
            transform.localScale = Vector3.zero;
        }
    }
    
    IEnumerator Anim(Animator animator)
    {
        yield return new WaitForSeconds(0.2f);
        animator.SetBool("isStone",true);
    }
    
    IEnumerator Destruction()
    {
        yield return new WaitForSeconds(0.4f);
        if (GetComponent<PhotonView>().IsMine)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }
}