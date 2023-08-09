using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2IdleState : StateMachineBehaviour
{
    float timer;
    float shootRange = 50;
    Transform player;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
        player = GameObject.FindGameObjectWithTag("PlayerCollider").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float distance = Vector3.Distance(animator.transform.position, player.position);
        if (distance < shootRange)
            animator.SetBool("isAttacking", true);
    }
}
