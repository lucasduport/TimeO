using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform TPpoint;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            if (collider.GetComponent<PhotonView>().IsMine)
            {
                collider.transform.position = TPpoint.position;
            }
        }
    }
}
