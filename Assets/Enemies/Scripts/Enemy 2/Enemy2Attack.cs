using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Attack : MonoBehaviour
{
    public Transform hitPoint;
    public GameObject fireBallPrefab;
    float shootTimer;

    Animator enemyAnimator;
    private void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        shootTimer = 0;
    }
    void Update()
    {
        shootTimer += Time.deltaTime;
        if (enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Claw Attack") && shootTimer >= 2)
        {
            shootFireBall();
            shootTimer = 0;
        }
    }

    void shootFireBall()
    {
        Instantiate(fireBallPrefab, hitPoint.position, Quaternion.identity);
    }
}
