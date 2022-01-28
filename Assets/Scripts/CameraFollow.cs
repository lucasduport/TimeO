using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public float timeOffset;
    public Vector3 posOffset;

    private Vector3 velocity;
    void Update()
    {
        //la camera effectue un glissé qui suit le joueur
        transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + posOffset,  ref velocity,
            timeOffset);
    }
}
