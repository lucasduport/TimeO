using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public InputField createInput;

    public InputField joinInput;

    public void CreateRoom()
    {
        //"a" est une valeur temporaire pour pouvoir tester plus vite
        //normalement la valeur en paramètre est createInput rentrée par le joueur
        PhotonNetwork.CreateRoom("a");
    }

    public void JoinRoom()
    {
        //"a" est une valeur temporaire pour pouvoir tester plus vite
        //normalement la valeur en paramètre est joinInput rentrée par le joueur
        PhotonNetwork.JoinRoom("a");
    }

    public override void OnJoinedRoom()
    {
        //chargement du premier niveau sur le server
        PhotonNetwork.LoadLevel("Level01");
    }
    
}
