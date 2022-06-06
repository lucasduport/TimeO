using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bed : MonoBehaviour
{
    public Transform newSpawn;

    public GameObject transition;
    public bool end = false;
    public GameObject musicToStop;

    public GameObject musicToStart;
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player") && collider.GetComponent<PhotonView>().IsMine)
        {
            collider.transform.position = newSpawn.position;
            collider.GetComponent<Animator>().SetBool("isStone",false);
            collider.GetComponent<Animator>().SetBool("isBranch",false);
            transition.SetActive(true);
            if (!end) StartCoroutine(Transition());
            musicToStop.SetActive(false);
            musicToStart.SetActive(true);
            
        }
    }

    IEnumerator Transition()
    {
        yield return new WaitForSeconds(1.5f);
        transition.SetActive(false);
    }
}
