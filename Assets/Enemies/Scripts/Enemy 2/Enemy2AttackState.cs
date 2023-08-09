using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2AttackState : StateMachineBehaviour
{
    Transform player;
    float attackRange = 50;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("PlayerCollider").transform;
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.LookAt(player);
        float distance = Vector3.Distance(animator.transform.position, player.position);

        if (distance > attackRange)
            animator.SetBool("isAttacking", false);
    }
}

