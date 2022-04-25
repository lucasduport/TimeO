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
    public static bool Spectate = false;
    

    private void Update()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (Spectate && players.Length != 0)
        {
            SpectateText.color = new Color(1f, 1f, 1f, 1f);
            GameObject.FindGameObjectWithTag("Player").transform.GetChild(1).gameObject.SetActive(true);
        }
        
        if (players.Length == 0)
        {
            PlayerCam.Clear();
            GravityEnabled = false;
            Spectate = false;
            SpectateText.color = new Color(1f, 1f, 1f, 0f);
            GameOver.SetActive(true);
            gameObject.SetActive(false);
        }
        else
        {
            if (GravityEnabled)
            {
                StartCoroutine(GravityTime());
            }
        }
    }

    IEnumerator GravityTime()
    {
        yield return new WaitForSeconds(3.5f);
        GravityEnabled = false;
    }


}