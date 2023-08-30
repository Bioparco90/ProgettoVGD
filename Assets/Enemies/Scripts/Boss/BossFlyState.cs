using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossFlyState : StateMachineBehaviour
{
    float attackRange;
    List<Transform> checkpoints = new List<Transform>();
    NavMeshAgent agent;
    Transform player;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        attackRange = 100;
        Transform checkpointsObj = GameObject.FindGameObjectWithTag("Checkpoints").transform;
        foreach (Transform t in checkpointsObj)
            checkpoints.Add(t);

        agent = animator.GetComponent<NavMeshAgent>();
        agent.SetDestination(checkpoints[0].position);
        player = GameObject.FindGameObjectWithTag("PlayerCollider").transform;
        agent.speed = 100;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float distance = Vector3.Distance(animator.transform.position, player.position);

        if (distance < attackRange)
        {
            animator.SetBool("isFlyAttacking", true);
        }
        if (agent.remainingDistance <= agent.stoppingDistance)
            agent.SetDestination(checkpoints[Random.Range(0, checkpoints.Count)].position);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(agent.transform.position);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
