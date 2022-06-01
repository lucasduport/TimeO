using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEditor;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    private Rigidbody2D rb; //le rigidbody du mob
    private Transform MTransform; //transform du mob
    private Animator Anim;
    public LayerMask layermask;

    public Transform groundCheck; // transform enfant du player qui détecte les colisions avec le sol

    private float horizontalmove;
    private Vector3 velocity = Vector3.zero;
    
    private bool isJumping;
    private bool isGrounded;

    // variables patrouille
    public Transform[] waypoints; // contient les deux bornes qui délimitent la zone de patrouille du mob
    private Vector3 target; // point ciblé pour la patrouille. Change quand il est atteint(alternance en waypoints[0] et waypoints[1]
    private int destpoint = 0; // coefficient qui prend les valeurs 0 ou 1 pour choisir la target 

    private void Start()
    {

        //Récupération des composants
        rb = GetComponent<Rigidbody2D>();
        MTransform = GetComponent<Transform>();
        Anim = GetComponent<Animator>();
        
        //patrouille
        target = waypoints[1].position;
        destpoint = 1;
    }

    void Update()
    {
        if (!GetComponent<PhotonView>().IsMine) return;
        
        //on regarde les collisions à l'intérieur du cerlce de rayon radius autour du groundCheck
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.3f, layermask);
                                                        //Mob est le nom du layer avec qui les detections ne seront pas faites
                                                        

        //gestion des animation ==> le joueur bouge horizontalement et n'es pas au sol = il marche
        Anim.SetBool("isWalking", Math.Abs(rb.velocity.x) > 0.1 && isGrounded);
        Anim.SetBool("isJumping", !isGrounded);

        isJumping = false;

        /*if (waypoints.Length == 2)
        {        
            if (destpoint == 0)
            {
                horizontalmove = -0.6f * 150 * Time.fixedDeltaTime;
            }
            else
            {
                horizontalmove = 0.6f * 150 * Time.fixedDeltaTime;
            }
        
            if (Vector2.Distance(transform.position, target) < 0.3f)
            {
                destpoint = (destpoint + 1) % 2; // changement de target lorsqu'il en atteint une
                target = waypoints[destpoint].position;

            }
        }*/
        transform.position = Vector3.MoveTowards(transform.position, target, 0.007f);// bouge jusqu'à la target
        if (MTransform.localScale.x >0 && destpoint%2 == 1)
        {
            destpoint = 0;
            target = waypoints[0].position;
        }

        if (MTransform.localScale.x < 0 && destpoint % 2 == 0)
        {
            
            destpoint = 1;
            target = waypoints[1].position;
        }

        if (Vector3.Distance(transform.position, target) < 0.3f)
        {
            destpoint = (destpoint + 1) % 2; // changement de target lorsqu'il en atteint une
            target = waypoints[destpoint].position;
            MTransform.localScale = new Vector3(-MTransform.localScale.x, MTransform.localScale.y, MTransform.localScale.z);
        }


    }
    // Update is called once per frame
    void FixedUpdate()
    {
        MoveMob(horizontalmove);

        //Flip(rb.velocity.x);
    }
    
    //MovePlayer() gère de le déplacement du joueur en fonction de _horizontalmove
    void MoveMob (float _horizontalmove)
    {
        Vector3 targetVelocity = new Vector2(_horizontalmove, rb.velocity.y);
        //le SmoothDamp permet de ne pas faire un déplacement trop bref mais légerement glissé
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, 0.15f);
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
        if (_rbvelocity < -0.1f && MTransform.localScale.x > 0)
        {
            //la scale est inversée, l'image est retournée
            MTransform.localScale = new Vector3(-MTransform.localScale.x,MTransform.localScale.y,MTransform.localScale.z);
        }
        else 
        {
            if (_rbvelocity > 0.1f && MTransform.localScale.x < 0)
            {
                MTransform.localScale = new Vector3(-MTransform.localScale.x,MTransform.localScale.y,MTransform.localScale.z);
            }
        }

    }
    // Je me garde cette fonction sous la main qui draw des guizmos c'est utile pour gérer le saut
    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position,0.3f);
    }

    */


    
}
