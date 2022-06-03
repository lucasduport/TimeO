using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class POVManager : MonoBehaviour, IPunObservable
{
    // Start is called before the first frame update
    public static bool GravityEnabled = false;
    public Text SpectateText;
    public GameObject GameOver;
    public static bool Spectate = false;
    public Transform spawnLimit;
    public Text RoomName;

    private void Start()
    {
        StartCoroutine(SpawnLimit(spawnLimit));
    }

    IEnumerator SpawnLimit(Transform transform)
    {
        yield return new WaitForSeconds(0.5f);
        if (transform.localScale.x < 0)
        {
            Spectate = true;
            foreach (var p in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (p.transform.position.x < transform.position.x)
                {
                    if (p.GetComponent<PhotonView>().IsMine)
                    {
                        PhotonNetwork.Destroy(p);
                    }
                }
            }
        }
    }
    private void Update()
    {
        RoomName.text = "Room name: " + PhotonNetwork.CurrentRoom.Name + "  ";
        if (spawnLimit.localScale.x < 0)
        {
            RoomName.color = Color.white;
        }
        else
        {
            RoomName.color = Color.green;
        }
        
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
    
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(GravityEnabled);
        }
        else
        {
            if (stream.IsReading)
            {
                GravityEnabled = (bool) stream.ReceiveNext();
            }
        }
    }


}