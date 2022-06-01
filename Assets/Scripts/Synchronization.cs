using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Synchronization : MonoBehaviour, IPunObservable
{
    
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
        }
        else
        {
            if (stream.IsReading)
            {
                
            }
        }
    }
    
}
