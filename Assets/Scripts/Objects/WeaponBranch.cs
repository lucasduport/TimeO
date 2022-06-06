using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Photon.Pun;
public class WeaponBranch : MonoBehaviour
{
    public AudioClip sound;
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player") && !collider.GetComponent<Animator>().GetBool("isBranch"))
        {
            Menu.instance.PlayClipAt(sound, transform.position);
            PhotonView pView = collider.GetComponent<PhotonView>();
            if (pView.IsMine)
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
    }

    IEnumerator Anim(Animator animator)
    {
        yield return new WaitForSeconds(0.2f);
        animator.SetBool("isBranch",true);
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
