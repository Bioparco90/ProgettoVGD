using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3IdleState : StateMachineBehaviour
{
    Transform player;
    float attackRange = 20;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("PlayerCollider").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.LookAt(player);
        float distance = Vector3.Distance(animator.transform.position, player.position);

        if (distance <= attackRange)
        {
            animator.SetBool("isWalking", true);
        }

        if (distance > 35)
        {
            animator.SetBool("isIdle", false);
        }
    }
}