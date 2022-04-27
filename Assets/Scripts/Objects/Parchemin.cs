using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class Parchemin : MonoBehaviour
{
    // Start is called before the first frame update
    public Text DisplayZone;
    public string Text = "";
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.CompareTag("Player"))
        {
            if (collider.transform.GetComponent<PhotonView>().IsMine)
            {
                gameObject.transform.GetChild(0).gameObject.SetActive(true);
                DisplayZone.text = Text;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.transform.CompareTag("Player"))
        {
            if (collider.transform.GetComponent<PhotonView>().IsMine)
            {
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }
}
