using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;


public class POVManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool GravityEnabled = false;
    public Text SpectateText;
    public GameObject GameOver;
    public static bool Spectate = false;
    
    private void Update()
    {
        if (Spectate)
        {
            GameOver.SetActive(false);
            StartCoroutine(FindCam());
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
        yield return new WaitForSeconds(3f);
        GravityEnabled = false;
    }

    IEnumerator FindCam()
    {
        yield return new WaitForSeconds(0.5f);
        try
        {
            SpectateText.color = new Color(1f, 1f, 1f, 1f);
            GameObject.FindGameObjectWithTag("Player").transform.GetChild(1).gameObject.SetActive(true);
        }
        catch (NullReferenceException)
        {
            GravityEnabled = false;
            Spectate = false;
            SpectateText.color = new Color(1f, 1f, 1f, 0f);
            GameOver.SetActive(true);
            gameObject.SetActive(false);
        }

    }


}