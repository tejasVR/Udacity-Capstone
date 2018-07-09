﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

    //public Transform player;

    public float distanceToMoveToPlayer = 5;
    public float distanceToAttackPlayer = 1;

    public float sightRange = 5;

    public float turnSpeed = .1f;

    public float enemyHealth = 2f;

    RagdollController rdController;

    public float distanceToPlayer;

    public NavMeshAgent agent;

    public bool _rayCastBeforeAttack;
    public bool _firstSeePlayer;

    LayerMask layerMask = ~0;

    public Transform startCast;
    public AudioSource[] _audioSources;

    private bool isDead;

    #region Attributes

    private Animator anim;

    private const string IDLE_BOOL = "idle";
    private const string MOVE_BOOL = "move";
    private const string ATTACK_BOOL = "attack";
    private const string HIT_BOOL = "hit";




    #endregion

    // Use this for initialization
    private void Awake()
    {
        rdController = GetComponent<RagdollController>();
    }

    void Start () {
        anim = GetComponent<Animator>();

        agent.updateRotation = false;
        //agent.updatePosition = false;

        //AudioSource.PlayClipAtPoint(audio.clip, )
        _audioSources[0].time = Random.Range(0f, 15f);

	}

    private void Update()
    {
        var dir = PlayerScript._playerEye.transform.position - startCast.position;
        dir.y = 0;
        distanceToPlayer = Vector3.Distance(startCast.position, new Vector3(PlayerScript._playerEye.transform.position.x, startCast.position.y, PlayerScript._playerEye.transform.position.z));

        //Debug.DrawRay(startCast.position, dir, Color.green);

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * turnSpeed);

        //anim.SetFloat("distanceToPlayer", distanceToPlayer);

        

        RaycastHit hit;
        Ray ray = new Ray(startCast.position, dir);

        //layerMask = ~layerMask;

        //if (!Physics.Raycast(ray, out hit, sightRange))
        {
            //anim.SetBool("firstSeePlayer", true);

            //print("I see the player!");
            //distanceToPlayer = Vector3.Distance(startCast.position, player.transform.position);
        }

        if (!anim.GetBool("hit"))
        {
            if (distanceToPlayer < distanceToMoveToPlayer && distanceToPlayer > distanceToAttackPlayer)
            {
                if (_rayCastBeforeAttack && !_firstSeePlayer)
                {
                    
                    if (!Physics.Raycast(ray, out hit, distanceToMoveToPlayer))
                    {
                        _firstSeePlayer = true;

                    }
                    else
                    {
                        //print("Enemy raycast hit: " + hit.collider.gameObject.name);
                    }
                }

                if (_rayCastBeforeAttack)
                {
                    if (_firstSeePlayer)
                        AnimateMove();
                }else
                {
                    AnimateMove();
                }

                agent.SetDestination(PlayerScript._playerEye.transform.position);
            }
            else if (distanceToPlayer <= distanceToAttackPlayer)
            {
                //dir.y = 0;
                AnimateAttack();
                //print("Attacking");
            }
            else
            {
                AnimateIdle();
            }
        }

        /*if (anim.GetBool("hit"))
        {
            print("hit is true");
        }*/

        
    }

    public void EnemyTakeHit(float damage, Vector3 hitDirection, GameObject hitBodyPart)
    {
        enemyHealth -= damage;
        if (enemyHealth <= 0)
        {
            Death(hitDirection, hitBodyPart);
            
        } else
        {
            AnimateHit();
            //print("hit animation");
            //anim.SetBool("hit", false);
        }

        //print(enemyHealth);
        //print("took damage");


    }

    private void Animate(string boolName)
    {
        DisableOtherAnimations(anim, boolName);

        anim.SetBool(boolName, true);
    }

    private void DisableOtherAnimations(Animator animator, string animation)
    {

        //animator.SetBool(IDLE_BOOL, false);
        //animator.SetBool(MOVE_BOOL, false);
        //animator.SetBool(ATTACK_BOOL, false);

        foreach (AnimatorControllerParameter parameter in animator.parameters)
        {
            if (parameter.name != animation)
            {
                animator.SetBool(parameter.name, false);
            }
        }
    }

    private void Death(Vector3 hitDirection, GameObject hitBodyPart)
    {
        isDead = true;
        rdController.KillRagdoll(hitDirection, hitBodyPart);

        foreach (var audio in _audioSources)
        {
            audio.Stop();
        }

        //this.gameObject.layer = 11; <<-- moved to ragdoll die method

    }

    private void OnAnimatorMove()
    {
        if(!isDead)
            agent.velocity = anim.deltaPosition / Time.deltaTime;
    }

    #region Animate Functions

    public void AnimateIdle()
    {
        Animate(IDLE_BOOL);
    }

    public void AnimateMove()
    {
        Animate(MOVE_BOOL);
    }

    public void AnimateAttack()
    {
        Animate(ATTACK_BOOL);
    }

    public void AnimateHit()
    {
        Animate(HIT_BOOL);
        //if (anim.GetBool("hit"))
            //print("hit is true");
        //anim.SetInteger("randomHit", Mathf.FloorToInt(Random.Range(1, 4)));
    }

    #endregion
}