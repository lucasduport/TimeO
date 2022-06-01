using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Bed : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            PhotonNetwork.LoadLevel("Level02");
        }
    }
}
