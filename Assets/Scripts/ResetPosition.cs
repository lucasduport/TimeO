using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class ResetPosition : MonoBehaviour
{
    public Transform spawnLoc;
    void Start()
    {
        foreach (var p in GameObject.FindGameObjectsWithTag("Player"))
        {
            p.transform.position = spawnLoc.position;
            p.GetComponent<Animator>().SetBool("isStone",false);
            p.GetComponent<Animator>().SetBool("isBranch",false);
        }
    }
}
