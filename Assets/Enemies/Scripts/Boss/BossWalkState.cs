using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class BossWalkState : StateMachineBehaviour
{
    float attackRange;
    Transform player;
    NavMeshAgent agent;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        attackRange = 20;
        player = GameObject.FindGameObjectWithTag("PlayerCollider").GetComponent<Transform>();
        agent = animator.GetComponent<NavMeshAgent>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.LookAt(player);
        float distance = Vector3.Distance(animator.transform.position, player.position);
        agent.SetDestination(player.position);

        if (distance < attackRange)
        {
            animator.SetBool("isAttacking", true);
        }

        if (distance > attackRange)
        {
            animator.SetBool("isWalking", false);
        }
    }

   
}
