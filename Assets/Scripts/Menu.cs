using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{


    public static Menu instance;
    // Start is called before the first frame update
    public GameObject GameOver;

    public AudioMixer mainMixer;
    public GameObject MenuPause;

    public GameObject Settings;
    private void Awake()
    {
        instance = this;
    }

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
    public AudioSource PlayClipAt(AudioClip clip, Vector3 pos)
    {
        GameObject tempgo = new GameObject("TempAudio");
        tempgo.transform.position = pos;
        AudioSource audioSource = tempgo.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.Play();
        audioSource.loop = true;
        Destroy(tempgo, clip.length);
        return audioSource;
    }
}