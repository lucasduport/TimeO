using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class WeaponObject : MonoBehaviour
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
            Destroy(gameObject);
        }
    }
}
