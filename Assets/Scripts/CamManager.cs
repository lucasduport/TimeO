using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class CamManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static Dictionary<string, Camera> PlayerCam = new Dictionary<string, Camera>();

    private void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Player").Length == 0)
        {

            PlayerCam.Clear();
            GravityObject.GravityEnabled = false;

            if (PhotonNetwork.InRoom) PhotonNetwork.LeaveRoom();
            PhotonNetwork.LoadLevel("Lobby");
        }
    }
}
