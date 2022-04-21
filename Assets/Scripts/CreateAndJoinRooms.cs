using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.UI;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public InputField createInput;

    public InputField joinInput;

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom("b");
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom("b");
    }

    public override void OnJoinedRoom()
    {
        //chargement du premier niveau sur le server si il y a seulement 1 autre joueur
        if (PhotonNetwork.PlayerList.Length <= 2)
        {
            PhotonNetwork.LoadLevel("Level01");
        }
        else
        {
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.LoadLevel("Lobby");
        }
    }
    

}
