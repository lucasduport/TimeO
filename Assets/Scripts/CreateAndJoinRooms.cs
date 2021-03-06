using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using Photon.Pun.Demo.Cockpit;
using UnityEngine.UI;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public InputField createInput;

    public InputField joinInput;

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom("e");
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom("e");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Level01");
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        PhotonNetwork.LocalPlayer.NickName = " ";
    }
}
