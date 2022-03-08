using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SpawnGameObjects : MonoBehaviour
{
    public GameObject[] ObjectsPrefab;
    public Transform[] SpawnLocation;

    // ATTENTION ObjectsPrefab.Length == SpawnLocation.Length
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.CompareTag("Player"))
        {
            //on instantantie les objects de la liste aux point qui leur correspond
            for (int i = 0; i < ObjectsPrefab.Length; i++)
            {
                PhotonNetwork.Instantiate(ObjectsPrefab[i].name, SpawnLocation[i].position, Quaternion.identity);
            }
        }
        //destruction du spawner (on ne veut pas que a chaque fois que le rigidbody soit trigger ça fasse spawn
        Destroy(this.gameObject);
    }
}