using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SpawnGameObjects : MonoBehaviour
{
    public GameObject[] ObjectsPrefab;
    public Transform[] SpawnLocation;

    // ATTENTION ObjectsPrefab.Length == SpawnLocation.Length sinon bugg
    void OnTriggerEnter2D(Collider2D collider)
    { 
        //on spwanera uniquement si on est bien sur la vue de celui qui est rentré dans le collider donc une seule fois
        if (collider.transform.GetComponent<PhotonView>().IsMine && collider.transform.CompareTag("Player"))
        {
            //on instantantie les objects de la liste aux point qui leur correspond
            for (int i = 0; i < ObjectsPrefab.Length; i++)
            {
                PhotonNetwork.Instantiate(ObjectsPrefab[i].name, SpawnLocation[i].position, Quaternion.identity);
            }
        }
        //destruction du spawner (on ne veut pas que a chaque fois que le rigidbody soit trigger ça fasse spawn
        Destroy(gameObject);
    }
}