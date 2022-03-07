using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject Player1Prefab;
    public GameObject Player2Prefab;
    public GameObject MobPrefab;

    public Transform SpawnLocation;

    // Start is called before the first frame update
    void Start()
    {
        //si le joueur est le second arrivé sur le server, il aura le style du perso2 et non de Timéo
        if (PhotonNetwork.PlayerList.Length == 1)
        {
            PhotonNetwork.Instantiate(Player1Prefab.name, SpawnLocation.position, Quaternion.identity);
            PhotonNetwork.Instantiate(MobPrefab.name, SpawnLocation.position, Quaternion.identity);
        }
        else
        {
            PhotonNetwork.Instantiate(Player2Prefab.name, SpawnLocation.position, Quaternion.identity);
        }
    }
}