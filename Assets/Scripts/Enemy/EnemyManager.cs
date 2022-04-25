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
    private Vector3 target; // point ciblé pour la patrouille.Change quand il est atteint(alternance en waypoints[0] et waypoints[1]
    private int destpoint = 0; // coefficient qui prend les valeurs 0 ou 1 pour choisir la target 

    // detection du joueur
    public bool IsCloseRange; // la détection se fait grâce à Overlaparea entre le coin haut gauche et bas droite de la zone
    public bool IsMidRange;
    public Transform CloseRangeHautGauche; 
    public Transform CloseRangeBasDroite;
    public Transform MidRangeHautGauche;
    public Transform MidRangeBasDroite;

    public LayerMask PlayerLayer; // permet d'avoir le layer du player pour la détection de joueur dans la close/mid range, afin de ne pas détecter le sol par ex.
    public Transform Player; // transform de timeo
    public Vector3 PlDest; //pour faire voler le joueur en chargeant
    private Vector3 Destination; //récupère le point à viser lors de la charge/attaque corps à corps
    // choix de l'action
    private System.Random rand = new System.Random(); // random pour choisir entre charge et attaque càc
    private float randomFloat;
    public float Char; //coefficient initialisé à 1 qui augmente si la charge est efficace et inversement
    private char LastAttack; //garde en mémoire la dernière attaque afin de faire varier Char
    public int CH = EnemyHealth.currentHealth; // récupère la vie de l'ennemi pour avoir l'efficacité des attaques


    //delimitation champ de vision de l'ennemi

    public Transform ChampDeVisionHG;
    public Transform ChampDeVisionBD;

    public bool InSightRange;// champ de vision. Fonctionne comme IsCloseRange

    void Patroll()
    {
      
        transform.position = Vector3.MoveTowards(transform.position, target,0.001f);// bouge jusqu'à la target
        
        if (Vector3.Distance(transform.position,target) < 0.3f)
        {
            destpoint = (destpoint + 1) % 2; // changement de target lorsqu'il en atteint une
            target = waypoints[destpoint].position;
            
        }
        
    }
    void Tempo ()
    {

        Destination = new Vector3(Player.position.x, -1f, 0); 
        transform.position = Vector3.MoveTowards(transform.position, Destination, 0.000001f); // fait un petit mouv pour faire une pause
    }
    
    
    void Charge()
    {
        Destination = new Vector3(Player.position.x, -1f, 0);

        transform.position = Vector3.MoveTowards(transform.position, Destination, 0.005f); //mouvement rapide 
        if (Vector3.Distance(transform.position, Destination) < 0.3f)
        {
            //HitPlayer
            /*PlDest = new Vector3(Player.position.x - 0.1f, Player.position.y + 0.1f, 0);
            Player.position = Vector3.MoveTowards(Player.position, PlDest, 0.01f);
            Invoke("Tempo", 5);*/
            // cette partie pose problème car il ne fait pas le test pendant mais après le déplacement
        }


    }
    void AttaqueCorpsACorps()
    {
        
        Destination = new Vector3(Player.position.x, -1f, 0);
        transform.position = Vector3.MoveTowards(transform.position, Destination, 0.002f); // mouvement moyen
        
    }

    private void Start()
    {
        //Récupération des composants
        rb = GetComponent<Rigidbody2D>();
        MTransform = GetComponent<Transform>();
        Anim = GetComponent<Animator>();
        
        //patrouille
        target = waypoints[0].position;

        
    }

    void Update()
    {
        //on regarde les collisions à l'intérieur du cerlce de rayon radius autour du groundCheck
        /*isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.3f, layermask);
                                                        //Mob est le nom du layer avec qui les detections ne seront pas faites
                                                     
        horizontalmove = 0 * 150 * Time.fixedDeltaTime;

        //gestion des animation ==> le joueur bouge horizontalement et n'es pas au sol = il marche
        Anim.SetBool("isWalking", Math.Abs(rb.velocity.x) > 0.1 && isGrounded);
        Anim.SetBool("isJumping", !isGrounded);

        isJumping = false;

        */
        Player = PlayerManager.PTransform; //récupère transform du joueur

        IsCloseRange = Physics2D.OverlapArea(CloseRangeHautGauche.position, CloseRangeBasDroite.position, PlayerLayer);
        IsMidRange = Physics2D.OverlapArea(MidRangeHautGauche.position, MidRangeBasDroite.position, PlayerLayer);
        InSightRange = Physics2D.OverlapArea(ChampDeVisionHG.position, ChampDeVisionBD.position, PlayerLayer);
        

        if (EnemyHealth.currentHealth < CH) //teste efficacité du coup
        {
            CH = EnemyHealth.currentHealth;
            if (LastAttack == 'a')
            {
                Char += (1 / 10);
            }
            if (LastAttack == 'c')
            {
                Char -= (1 / 10);
            }

        }

        if (!InSightRange) // si le joueur n'est pas sur la plateforme de l'ennemi ce dernier patrouille
        {
            Patroll();
        }
        else if (!IsMidRange) //si le joueur est loin l'enni fait une pause et charge
        {
            Invoke("Charge",2);
            LastAttack = 'c'; // la dernière attaque devient une charge
        }
        else if (IsCloseRange) // si le joueur et contact l'ennemi lance une attaque corps à corps
        {
           
            AttaqueCorpsACorps();
            LastAttack = 'a';// la dernière attaque devient une attaque corps à corps
        }
        else // si le joueur est mid range 
        {
            randomFloat = (float)rand.NextDouble();
            if (randomFloat * Char < 0.5) // on multiplie le nombre aléatoire par char ce qui a tendance à renvoyer le meilleur coup mais pas tout le temps non plus
            {
                AttaqueCorpsACorps();
                LastAttack = 'a';
            }
            else
            {
                Charge();
                LastAttack = 'c';
            }

        }
        Invoke("Tempo", 3); // pause à la fin



    }
    // Update is called once per frame
    /*void FixedUpdate()
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
    */// Je me garde cette fonction sous la main qui draw des guizmos c'est utile pour gérer le saut
    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position,0.3f);
    }

    */


    
}
