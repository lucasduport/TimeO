using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class WeaponKhepesh : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player") && !collider.GetComponent<Animator>().GetBool("isKhepesh"))
        {
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
        animator.SetBool("isKhepesh",true);
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
