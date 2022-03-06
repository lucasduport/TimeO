using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb; //le rigidbody du player
    private Transform PTransform; //transform du player

    public Transform groundCheck; // transform enfant du player qui détecte les colisions avec le sol

    public Animator Anim;
    
    private float horizontalmove;
    private Vector3 velocity = Vector3.zero;
    
    private bool isJumping;
    private bool isGrounded;

    public Camera m_cam;
    private PhotonView _view;

    private void Start()
    {
        //Récupération des composants
        rb = GetComponent<Rigidbody2D>();
        PTransform = GetComponent<Transform>();
        
        _view = GetComponent<PhotonView>();
        
        //méthode de PhotonView qui permait de savoir si l'on est bien sur la vue du joueur concerné
        if (_view.IsMine)
        {
            //par défaut les cam enfants des préabs joueurs sont désactivées : si la vue et la notre alors on les active
            m_cam.gameObject.SetActive(true);
        }
    }

    void Update()
    {
        if (_view.IsMine)
        {
            //on regarde les collisions à l'intérieur du cerlce de rayon radius autour du groundCheck
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.3f, LayerMask.NameToLayer("Player"));
                                                        //default est le nom du layer avec qui les detections seront faites
            
            //le joueur appuie sur q ou d pour se déplacer, on obtient la valeur du mouvement horizontale
            horizontalmove = Input.GetAxis("Horizontal") * 150 * Time.fixedDeltaTime;

            //gestion des animation ==> le joueur bouge horizontalement et n'es pas au sol = il marche
            Anim.SetBool("isWalking", Math.Abs(rb.velocity.x) > 0.1 && isGrounded);
            Anim.SetBool("isJumping", !isGrounded);


            
            if (Input.GetButtonDown("Jump") && isGrounded && Math.Abs(rb.velocity.y) < 0.5)
            {
                isJumping = true;
            }
            
            
        }
    }

    void FixedUpdate()
    {
        MovePlayer(horizontalmove);

        Flip(rb.velocity.x);
    }

    //MovePlayer() gère de le déplacement du joueur en fonction de _horizontalmove
    void MovePlayer (float _horizontalmove)
    {
        Vector3 targetVelocity = new Vector2(_horizontalmove, rb.velocity.y);
        //le SmoothDamp permet de ne pas faire un déplacement trop bref mais légerement glissé
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, 0.05f);
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
            //la scale est inversée, l'image est retournée
            PTransform.localScale = new Vector3(-0.5f,PTransform.localScale.y,PTransform.localScale.z);
        }
        else 
        {
            if (_rbvelocity > 0.1f)
            {
                PTransform.localScale = new Vector3(0.5f,PTransform.localScale.y,PTransform.localScale.z);
            }
        }

    }

    /* Je me garde cette fonction sous la main qui draw des guizmos c'est utile pour gérer le saut du joueur
     
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position,0.3f);
    }
    */
}
