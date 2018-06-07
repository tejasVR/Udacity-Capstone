using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

    public Transform player;

    public float distanceToMoveToPlayer = 5;
    public float distanceToAttackPlayer = 1;

    public float turnSpeed = .1f;

    public float enemyHealth = 2f;

    RagdollController rdController;

    public float distanceToPlayer;

    public NavMeshAgent agent;

    #region Attributes

    private Animator animator;

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
        animator = GetComponent<Animator>();

        agent.updateRotation = false;
        //agent.updatePosition = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        Debug.DrawRay(this.transform.position, player.position, Color.green);

        //RaycastHit hit;
        var dir = transform.position - player.position;
        dir.y = 0;

        if (Physics.Raycast(transform.position, dir))
        {
            distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer < distanceToMoveToPlayer && distanceToPlayer > distanceToAttackPlayer)
            {
                
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * turnSpeed);
                AnimateMove();

                agent.SetDestination(player.transform.position);

            }

            if (distanceToPlayer <= distanceToAttackPlayer)
            {
                //dir.y = 0;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * turnSpeed);
                AnimateAttack();
            }

        }
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
    }

    #endregion

    public void EnemyTakeHit(float damage, Vector3 hitDirection, GameObject hitBodyPart)
    {
        enemyHealth -= damage;
        if (enemyHealth <= 0)
        {
            Death(hitDirection, hitBodyPart);
            
        } else
        {
            AnimateHit();
        }

        //print(enemyHealth);
        print("took damage");


    }

    private void Animate(string boolName)
    {
        DisableOtherAnimations(animator, boolName);

        animator.SetBool(boolName, true);
    }

    private void DisableOtherAnimations(Animator animator, string animation)
    {
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
        rdController.KillRagdoll(hitDirection, hitBodyPart);
        this.gameObject.layer = 11;
        this.enabled = false;

    }

    private void OnAnimatorMove()
    {
        agent.velocity = animator.deltaPosition / Time.deltaTime;
    }
}
