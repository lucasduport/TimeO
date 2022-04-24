using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class WeaponBranch : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.CompareTag("Player"))
        {
            if (collider.transform.GetComponent<PhotonView>().IsMine && !collider.transform.GetComponent<Animator>().GetBool("isBranch"))
            {
                Animator ph = collider.transform.GetComponent<Animator>();
                ph.SetBool("isBranch", true);
                PhotonNetwork.Destroy(gameObject);
            }
            
        }
    }
}
