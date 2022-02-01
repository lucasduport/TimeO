using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody2D rb; //le rigidbody du player
    
    public SpriteRenderer spriteRenderer; //image du player
    public LayerMask collisionLayers; //Layer avec lequel jump doit détecter les collisions
    
    public Transform groundCheck; // transform enfant du player qui détecte les colisions avec le sol

    public Animator Anim;
    private float horizontalmove;
    private bool isJumping;
    private bool isGrounded;
    private Vector3 velocity = Vector3.zero;

    private PhotonView _view;

    private void Start()
    {
        _view = GetComponent<PhotonView>();
        Photon.Pun.Demo.PunBasics.CameraWork _cameraWork = this.gameObject.GetComponent<Photon.Pun.Demo.PunBasics.CameraWork>();
        if (_view.IsMine)_cameraWork.OnStartFollowing();
    }

    void Update()
    {
        if (_view.IsMine)
        {
            //on regarde les collisions à l'intérieur du cerlce de rayon radius autour du groundCheck
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.3f, collisionLayers);

            //le joueur appuie sur q ou d pour se déplacer, on obtient la valeur du mouvement horizontale
            horizontalmove = Input.GetAxis("Horizontal") * 150 * Time.fixedDeltaTime;


            Anim.SetBool("isWalking", horizontalmove != 0 && isGrounded);



            if (Input.GetButtonDown("Jump") && isGrounded && velocity.y == 0f)
            {
                isJumping = true;
            }
            
            
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (_view.IsMine)
        {
            MovePlayer(horizontalmove);
            Flip(rb.velocity.x);
        }
    }

    //MovePlayer() gère de le déplacement du joueur en fonction de _horizontalmove
    void MovePlayer (float _horizontalmove)
    {
        Vector3 targetVelocity = new Vector2(_horizontalmove, rb.velocity.y);
        //le SmoothDamp permet de ne pas faire un déplacement trop bref mais légerement glissé
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);

        if (isJumping)
        {
            //On pousse le rigidbody vers le haut
            rb.AddForce(new Vector2(0f, 250f));
            isJumping = false;
        }
    }

    //Flip() qui prend en paramètre la vitesse du RigidBody, permet de savoir dans quel sens se déplace le perso et ainsi
    //de retourner l'image si besoin
    void Flip(float _rbvelocity)
    {
        if (_rbvelocity < -0.1f)
        {
            spriteRenderer.flipX = true;
        }
        else 
        {
            if (_rbvelocity > 0.1f)
            {
                spriteRenderer.flipX = false;
            }
        }
        
        void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(spriteRenderer.flipX);
            }
            else
            {
                spriteRenderer.flipX = (bool) stream.ReceiveNext();
            }
        }
        
    }

    /* Je me garde cette fonction sous la main qui draw des guizmos c'est utile pour gérer le saut
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position,0.3f);
    }
    */
}
