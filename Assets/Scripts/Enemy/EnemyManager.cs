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
    private float speed = 3/2;
    public Transform[] waypoints;
    private Transform target;
    private int destpoint = 0;

    // detection du joueur
    public bool IsCloseRange;
    public bool IsMidRange;


    public Transform CloseRangeHautGauche;
    public Transform CloseRangeBasDroite;
    public Transform MidRangeHautGauche;
    public Transform MidRangeBasDroite;
    public LayerMask PlayerLayer;
    public Transform Player;

    // choix de l'action
    private System.Random rand = new System.Random();
    private float randomFloat;
    public float Char; //Charge
    private char LastAttack;
    public int CH = EnemyHealth.currentHealth;


    //delimitation champ de vision de l'ennemi

    public Transform ChampDeVisionHG;
    public Transform ChampDeVisionBD;

    public bool InSightRange;

    void Patroll()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position,target.position) < 0.3f)
        {
            destpoint = (destpoint + 1) % 2;
            target = waypoints[destpoint];

        }
    }
    
    
    
    void Charge()
    {

        Vector3 dire = Player.position - transform.position;
        transform.Translate(dire.normalized * speed * 2 * Time.deltaTime, Space.World);
    }
    void AttaqueCorpsACorps()
    {
        
        Vector3 dire = Player.position - transform.position;
        transform.Translate(dire.normalized * speed * Time.deltaTime, Space.World);


    }
    IEnumerator LoadDelayed()
    {
        yield return new WaitForSeconds(2);
        Charge();
    }



    private void Start()
    {
        //Récupération des composants
        rb = GetComponent<Rigidbody2D>();
        MTransform = GetComponent<Transform>();
        Anim = GetComponent<Animator>();

        //patrouille
        target = waypoints[0];
    }

    void Update()
    {
        //on regarde les collisions à l'intérieur du cerlce de rayon radius autour du groundCheck
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.3f, layermask);
                                                        //Mob est le nom du layer avec qui les detections ne seront pas faites
                                                     
        horizontalmove = 0 * 150 * Time.fixedDeltaTime;

        //gestion des animation ==> le joueur bouge horizontalement et n'es pas au sol = il marche
        Anim.SetBool("isWalking", Math.Abs(rb.velocity.x) > 0.1 && isGrounded);
        Anim.SetBool("isJumping", !isGrounded);

        isJumping = false;




        IsCloseRange = Physics2D.OverlapArea(CloseRangeHautGauche.position, CloseRangeBasDroite.position, PlayerLayer);
        IsMidRange = Physics2D.OverlapArea(MidRangeHautGauche.position, MidRangeBasDroite.position, PlayerLayer);
        InSightRange = Physics2D.OverlapArea(ChampDeVisionHG.position, ChampDeVisionBD.position, PlayerLayer);


        if(EnemyHealth.currentHealth < CH)
        {
            if (LastAttack == 'a')
            {
                Char += (1 / 10);
            }
            if (LastAttack == 'c')
            {
                Char -= (1 / 10);
            }

        }

        if (!InSightRange)
        {
            Patroll();
        }
        else if (!IsMidRange)
        {
            StartCoroutine(LoadDelayed());
            LastAttack = 'c';
        }
        else if (IsCloseRange)
        {
           
            AttaqueCorpsACorps();
            LastAttack = 'a';
        }
        else
        {
            randomFloat = (float)rand.NextDouble();
            if (randomFloat * Char < 0.5) 
            {
                AttaqueCorpsACorps();
                LastAttack = 'a';
            }
            else
            {
                StartCoroutine(LoadDelayed());
                LastAttack = 'c';
            }

        }

        
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        MoveMob(horizontalmove);

        Flip(rb.velocity.x);
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
        if (_rbvelocity < -0.1f && MTransform.localScale.x < 0)
        {
            //la scale est inversée, l'image est retournée
            MTransform.localScale = new Vector3(-MTransform.localScale.x,MTransform.localScale.y,MTransform.localScale.z);
        }
        else 
        {
            if (_rbvelocity > 0.1f && MTransform.localScale.x > 0)
            {
                MTransform.localScale = new Vector3(-MTransform.localScale.x,MTransform.localScale.y,MTransform.localScale.z);
            }
        }

    }
    /*// Je me garde cette fonction sous la main qui draw des guizmos c'est utile pour gérer le saut
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position,0.3f);
    }*/




    
}
