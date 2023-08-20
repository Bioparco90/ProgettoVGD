using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy3Attack : MonoBehaviour
{
    public Transform hitPoint;
    public float attackRange = 7.0f;
    public LayerMask playerLayer;
    float timer;

    Animator enemyAnimator;

    private void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        timer = 0;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Basic Attack") && timer >= 1)
        {
            Attack();
            timer = 0;
        }
    }
    void Attack()
    {
        Collider[] hitPlayer = Physics.OverlapSphere(hitPoint.position, attackRange, playerLayer);
        foreach (Collider player in hitPlayer)
        {
            player.GetComponent<PlayerController>().takeDamage(50);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (hitPoint == null)
            return;
        Gizmos.DrawWireSphere(hitPoint.position, attackRange);
    }


}
