using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject Player1Prefab;
    public GameObject Player2Prefab;

    public Transform SpawnLocation;

    private bool isTimeo;

    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.PlayerList.Length > 2)
        {
            POVManager.Spectate = true;
        }
        else
        {
            isTimeo = false;
            foreach (var player in PhotonNetwork.PlayerList)
            {
                if (player.NickName == "Timéo")
                {
                    isTimeo = true;
                }
            }
            
            //si le joueur est le second arrivé sur le server, il aura le style du perso2 et non de Timéo
            if (!isTimeo)
            {
                PhotonNetwork.Instantiate(Player1Prefab.name, SpawnLocation.position, Quaternion.identity);
                //je lui met le nickname de Timéo pour savoir quel joueur quitte la partie
                PhotonNetwork.PlayerList[PhotonNetwork.PlayerList.Length - 1].NickName = "Timéo";
            }
            else
            {
                PhotonNetwork.Instantiate(Player2Prefab.name, SpawnLocation.position - new Vector3(2f, 0f, 0f),
                    Quaternion.identity);
            }
        }
    }

}