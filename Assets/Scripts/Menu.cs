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
    public GameObject MenuPause;

    public GameObject Settings;
    

    public void Update()
    {
        if (Input.GetKeyDown((KeyCode) 27))
        {
            if (MenuPause.activeSelf)
            {
                {
                    Resume();
                }
            }
            else
            {
                if (Settings.activeSelf)
                {
                    Back();
                }
                else
                {
                    Pause();
                }
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
        mainMixer.SetFloat("volume", volume);
    }

    public void Back()
    {
        MenuPause.SetActive(true);
        Settings.SetActive(false);
    }

    public void Panel()
    {
        Settings.SetActive(true);
        MenuPause.SetActive(false);
    }

    public void Pause()
    {
        MenuPause.SetActive(true);
    }
}