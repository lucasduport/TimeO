using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;
using UnityEditor;


public class BossManager : MonoBehaviour
{
    public Transform[] waypoints; // contient les deux bornes qui d�limitent la zone de patrouille du mob
    private Vector3 target; // point ciblé pour la patrouille.Change quand il est atteint(alternance en waypoints[0] et waypoints[1]
    private int destpoint; // coefficient qui prend les valeurs 0 ou 1 pour choisir la target 

    public Transform MTransform; //transform du mob
    private Animator Anim;

    public Transform ChampDeVisionHD; // la d�tection se fait grâce a Overlaparea entre le coin haut gauche et bas droite de la zone
    public Transform ChampDeVisionBG;
    
    private bool IsCloseRange; 
    private bool IsMidRange;
    private bool InSightRange;

    public Transform CloseRangeBasGauche;
    public Transform CloseRangeHautDroite;
    public Transform MidRangeBasGauche;
    public Transform MidRangeHautDroite;
    public LayerMask PlayerLayer;

    float val = 999999;
    private Transform ClosestPlayer;
    private bool IsWaitingToCharge;
    private float t;
    private Vector3 PosToGo;
    private bool IsCharging;

    private bool IsCloseAttacking;

    private bool HasAttacked;

    private System.Random r;
    private double Charge;

    public float patrouille;
    public float chargev;
    public float cac;

    // Start is called before the first frame update
    void Start()
    {
        //patrouille
        target = waypoints[1].position;
        destpoint = 1;
        Charge = 1;
        r = new System.Random();
    }

    // Update is called once per frame
    void Update()
    {
        IsCloseRange = Physics2D.OverlapArea(CloseRangeBasGauche.position, CloseRangeHautDroite.position, PlayerLayer);
        IsMidRange = Physics2D.OverlapArea(MidRangeBasGauche.position, MidRangeHautDroite.position, PlayerLayer);
        InSightRange = Physics2D.OverlapArea(ChampDeVisionBG.position, ChampDeVisionHD.position, PlayerLayer);

        if (!InSightRange) // patrouille
        {
            
            IsCharging = false;
            IsCloseAttacking = false;
            IsWaitingToCharge = false;

            transform.position = Vector3.MoveTowards(transform.position, target, patrouille);// vitesse patrouille
            if (MTransform.localScale.x > 0 && destpoint % 2 == 1)
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
        else
        {
           
            GameObject[] joueurs = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject go in joueurs)
            {
                if (Math.Abs(go.transform.position.x) < Math.Abs(val))
                {
                    val = go.transform.position.x;
                    ClosestPlayer = go.transform;
                }
            }
            if (HasAttacked)
            {
                //vient d'attaquer
                if (Time.time-t > 2.5f)
                {
                    HasAttacked = false;
                    
                }
            }
            else if ((!IsMidRange && !IsCloseAttacking) || IsWaitingToCharge || IsCharging ) // charge
            {
                if (!IsWaitingToCharge && !IsCharging)
                {
                    //d�but pause avant la charge
                    IsWaitingToCharge = true;
                    t = Time.time;
                    

                }
                else
                {
                    if (IsCharging && Time.time-t <3f)
                    {
                        // charge
                        if (Vector3.Distance(transform.position, PosToGo) < 0.3f) // fin de la charge si on arrive au bout de la zone
                        {
                            IsCharging = false;
                            HasAttacked = true;
                            t = Time.time;
                        }
                        else
                        {
                            transform.position = Vector3.MoveTowards(transform.position, PosToGo, chargev); //vitesse charge
                        }
                      
                    }
                    else if (IsCharging)
                    {
                        // fin de la charge
                        IsCharging = false; 
                        HasAttacked = true;
                        t = Time.time;
                    }
                    else if (Time.time - t > 2f)
                    {
                        // sur le point de charger
                        IsCharging = true; 
                        IsWaitingToCharge = false;
                        t = Time.time;
                        if(ClosestPlayer.position.x <= MTransform.position.x)
                        {
                            PosToGo = waypoints[0].position;
                            transform.localScale = Vector3.one;
                        }
                        else
                        {
                            PosToGo = waypoints[1].position;
                            transform.localScale = new Vector3(-1, 1, 1);
                        }
                        
                    }
                }
            }
            else if ((IsCloseRange && !(IsWaitingToCharge || IsCharging)) || IsCloseAttacking) // attaque corps à corps
            {
                if (!IsCloseAttacking) // debut de l'attaque
                {
                    IsCloseAttacking = true;
                    t = Time.time;
                }
                else if (Time.time-t < 3f) // attaque
                {
                    if (ClosestPlayer.position.x <= MTransform.position.x)
                    {
                        PosToGo = waypoints[0].position;
                    }
                    else
                    {
                        PosToGo = waypoints[1].position;
                    }

                    transform.position = Vector3.MoveTowards(transform.position, PosToGo, cac); // vitesse attaque corps à corps
                    
                }
                else // fin de l'attaque
                {
                    IsCloseAttacking = false;
                    HasAttacked = true;
                    t = Time.time;
                }
                
            }
            else if (IsMidRange)
            {
                if (r.Next(10)*Charge >= 5)
                {
                    IsWaitingToCharge = true;
                    t = Time.time;
                }
                else
                {
                    IsCloseAttacking = true;
                    t = Time.time;
                }
            }
        }
        GetComponent<Animator>().SetBool("isWalking", !IsWaitingToCharge);
    }                    
}
