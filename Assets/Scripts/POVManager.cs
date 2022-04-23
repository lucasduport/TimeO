using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;


public class POVManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static Dictionary<string, Camera> PlayerCam = new Dictionary<string, Camera>();
    public static bool GravityEnabled = false;
    public Text SpectateText;
    public GameObject GameOver;

    private void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Player").Length == 0)
        {
            PlayerCam.Clear();
            GravityEnabled = false;
            SpectateText.color = new Color(1f, 1f, 1f, 0f);
            GameOver.SetActive(true);
        }
    }
    
    public void LoadMainMenu()
    {
        GameOver.SetActive(false);
        if (PhotonNetwork.InRoom) PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel("Lobby");
    }
}


