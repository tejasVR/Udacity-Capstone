using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public Transform player;

    public float distanceToMoveToPlayer = 5;
    public float distanceToAttackPlayer = 1;

    public float turnSpeed = .1f;

    #region Attributes

    private Animator animator;

    private const string IDLE_BOOL = "idle";
    private const string MOVE_BOOL = "move";
    private const string ATTACK_BOOL = "attack";
    private const string HIT_BOOL = "hit";


    #endregion

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        //RaycastHit hit;
        var dir = player.position - transform.position;

        if(Physics.Raycast(transform.position, dir))
        {
            if (Vector3.Distance(transform.position, player.position) < distanceToMoveToPlayer)
            {
                dir.y = 0;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * turnSpeed);
                AnimateMove();

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

    #endregion

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
}
