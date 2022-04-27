using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class HealObject : MonoBehaviour
{
    // Start is called before the first frame update
    public int heal;
    void OnTriggerEnter2D(Collider2D collider)
    {
        if ( collider.CompareTag("Player"))
        {
            if (collider.GetComponent<PhotonView>().IsMine)
            {
                PlayerHealth ph = collider.GetComponent<PlayerHealth>();
                ph.HealthModifications(heal);
                GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 0f);
            }

            if (GetComponent<PhotonView>().IsMine)
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }
}
