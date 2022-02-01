using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject Player1Prefab;
    public GameObject Player2Prefab;

    public Transform SpawnLocation;

    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.PlayerList.Length == 1)
        {
            PhotonNetwork.Instantiate(Player1Prefab.name, SpawnLocation.position, Quaternion.identity);
        }
        else
        {
            PhotonNetwork.Instantiate(Player2Prefab.name, SpawnLocation.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
}