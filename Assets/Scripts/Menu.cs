using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Audio;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject GameOver;

    public AudioMixer mainMixer;
    public GameObject Pause;

    public static GameObject MenuPause;

    public GameObject Settings;
    
    public void Start()
    {
        MenuPause = Pause;
    }

    public void FixedUpdate()
    {
        if (Input.GetKeyDown((KeyCode)27))
        {
            if (Settings.activeSelf)
            {
                Back();
            }
            else
            {
                Resume();
            }
        }
    }

    public void LoadMainMenu()
    {
        GameOver.SetActive(false);
        if (PhotonNetwork.InRoom) PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel("Lobby");
    }

    public void Resume()
    {
        MenuPause.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetVolume(float volume)
    {
        mainMixer.SetFloat("volume",volume);
    }

    public void Back()
    {
        Settings.SetActive(false);
        Pause.SetActive(true);
    }

    public void Panel()
    {
        Settings.SetActive(true);
        Pause.SetActive(false);
    }
}