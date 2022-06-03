using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class SpawnLimit : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            if (GetComponent<PhotonView>().IsMine) transform.localScale = -Vector3.one;
            gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }
}
