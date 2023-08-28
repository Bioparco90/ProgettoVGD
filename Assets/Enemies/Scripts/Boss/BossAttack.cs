using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public Transform hitPoint;
    public Transform hitPointFireball;

    public float attackRange = 7.0f;
    public LayerMask playerLayer;
    float timer;
    public GameObject fireBallPrefab;

    Animator enemyAnimator;

    private void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        timer = 0;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Claw Attack") && timer >= 1)
        {
            MeleeAttack();
            timer = 0;
        }

        if (enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Fly Flame Attack") && timer >= 1)
        {
            ShootFireBall();
            timer = 0;
        }
    }







    void MeleeAttack()
    {
        Collider[] hitPlayer = Physics.OverlapSphere(hitPoint.position, attackRange, playerLayer);
        foreach (Collider player in hitPlayer)
        {
            player.GetComponent<PlayerController>().takeDamage(35);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (hitPoint == null)
            return;
        Gizmos.DrawWireSphere(hitPoint.position, attackRange);
    }

    void ShootFireBall()
    {
        Instantiate(fireBallPrefab, hitPointFireball.position, Quaternion.identity);
    }

}
