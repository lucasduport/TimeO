using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using Photon.Pun;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class PlayerManager : MonoBehaviour
{

    private Rigidbody2D rb; //le rigidbody du player
    private Transform PTransform; //transform du player

    public Transform groundCheck; //transform enfant du player qui détecte les collisions avec le sol

    public Animator Anim;
    
    private float horizontalmove;
    private Vector3 velocity = Vector3.zero;
    
    private bool isJumping;
    private bool isGrounded;

    public GameObject branch;
    
    public Camera m_cam;
    public Canvas canvas;
    private PhotonView _view;


    private void Start()
    {
        //Récupération des composants
        rb = GetComponent<Rigidbody2D>();
        PTransform = GetComponent<Transform>();
        
        _view = GetComponent<PhotonView>();
        
        POVManager.PlayerCam.Add(gameObject.name,m_cam);
        
        //méthode de PhotonView qui permait de savoir si l'on est bien sur la vue du joueur concerné
        if (_view.IsMine)
        {
            //par défaut les cam enfants des préabs joueurs sont désactivées : si la vue et la notre alors on les active
            m_cam.gameObject.SetActive(true);
            canvas.gameObject.SetActive(true);
        }
    }

    void Update()
    {
        if (Anim.GetBool("killed"))
        {
            if (Anim.GetBool("isBranch"))
            {
                Drop();
                PhotonNetwork.Destroy(gameObject);
            }
            else
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }

        if (_view.IsMine)
        {
            //on regarde les collisions à l'intérieur du cerlce de rayon radius autour du groundCheck
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.3f, LayerMask.NameToLayer("Player"));
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            if (players.Length == 2)
            {
                GameObject otherPlayer = gameObject;
                foreach (var p in players)
                {
                    if (p != gameObject) otherPlayer = p;
                }
                isGrounded = isGrounded || GetComponent<CapsuleCollider2D>().IsTouching(otherPlayer.GetComponent<CapsuleCollider2D>());
            }

            //le joueur appuie sur q ou d pour se déplacer, on obtient la valeur du mouvement horizontale
            horizontalmove = Input.GetAxis("Horizontal") * 150 * Time.fixedDeltaTime;

            //gestion des animation ==> le joueur bouge horizontalement et n'es pas au sol = il marche
            Anim.SetBool("isWalking", Math.Abs(rb.velocity.x) > 0.1 && isGrounded);
            Anim.SetBool("isJumping", !isGrounded);
            
            //si l'orbe de gravité est prise alors un timer se lance jusqu'à la fin
            if (POVManager.GravityEnabled) StartCoroutine(GravityTime());
            
            //isHit doit être vraie seulement une frame car l'anim se joue jusqu'à la fin quoiqu'il arrive
            if (Anim.GetBool("isHit")) Anim.SetBool("isHit",false);

            //active l'enfant du player "branch" qui detecte les collisions avec les ennemis (système de coup)
            branch.SetActive(Anim.GetCurrentAnimatorStateInfo(0).IsName("hit_branch"));

            if (Input.GetButtonDown("Fire1"))
            {
                Hit();
            }
            if (Input.GetButtonDown("Jump") && isGrounded && Math.Abs(rb.velocity.y) < 0.5)
            {
                isJumping = true;
            }

            if (Input.GetKeyDown("g"))
            {
                Drop();
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
            if (POVManager.GravityEnabled)
            {
                rb.AddForce(new Vector2(0f, 400f));
            }
            else
            {
                //On pousse le rigidbody vers le haut
                rb.AddForce(new Vector2(0f, 250f));
            }
            isJumping = false;
        }
    }

    //Flip() qui prend en paramètre la vitesse du RigidBody, permet de savoir dans quel sens se déplace le perso et ainsi
    //de retourner l'image si besoin
    void Flip(float _rbvelocity)
    {
        if (_rbvelocity < -0.1f && PTransform.localScale.x > 0)
        {
            //la scale est inversée, l'image est retournée
            PTransform.localScale = new Vector3(-PTransform.localScale.x,PTransform.localScale.y,PTransform.localScale.z);
        }
        else 
        {
            if (_rbvelocity > 0.1f && PTransform.localScale.x < 0)
            {
                PTransform.localScale = new Vector3(-PTransform.localScale.x,PTransform.localScale.y,PTransform.localScale.z);
            }
        }

    }

    void Hit()
    {
        Anim.SetBool("isHit",true);
    }
    void Drop()
    {
        if (Anim.GetBool("isBranch"))
        {
            Anim.SetBool("isBranch", false);
            if (PTransform.localScale.x == -0.5f)
            {
                PhotonNetwork.Instantiate("Branch", PTransform.position + new Vector3(-0.7f, 0f, 0f),Quaternion.identity);
            }
            else
            {
                PhotonNetwork.Instantiate("Branch", PTransform.position + new Vector3(0.7f, 0f, 0f),Quaternion.identity);
            }
            
        }
    }
    IEnumerator GravityTime()
    {
        yield return new WaitForSeconds(3.5f);
        POVManager.GravityEnabled = false;
    }
    
    /*
    private void OnDrawGizmos() 
    {
       Gizmos.color = Color.red;
       Gizmos.DrawWireSphere(groundCheck.position,0.3f); 
    }
    */

}
