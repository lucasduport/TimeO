using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SpawnGameObjects : MonoBehaviour
{
    public GameObject[] ObjectsPrefab;
    public Transform[] SpawnLocation;

    // ATTENTION ObjectsPrefab.Length == SpawnLocation.Length sinon bugg
    void OnTriggerEnter2D(Collider2D collider)
    { 
        //on spwanera uniquement si on est bien sur la vue de celui qui est rentré dans le collider donc une seule fois
        if (collider.transform.CompareTag("Player"))
        {
            if (collider.GetComponent<PhotonView>().IsMine)
            {
                //on instantantie les objects de la liste aux point qui leur correspond
                for (int i = 0; i < ObjectsPrefab.Length; i++)
                {
                    PhotonNetwork.Instantiate(ObjectsPrefab[i].name, SpawnLocation[i].position, Quaternion.identity);
                }
            }
            gameObject.transform.localScale = Vector3.zero;
            foreach (var c in gameObject.GetComponents<Collider2D>())
            {
                c.enabled = false;
            }
            StartCoroutine(Destruction());
        }
        
        IEnumerator Destruction()
        {
            yield return new WaitForSeconds(1);
            if (gameObject.GetComponent<PhotonView>().IsMine &&
                gameObject.transform.localScale == Vector3.zero)
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }
}