using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Attack : MonoBehaviour
{
    public Transform hitPoint;
    public GameObject fireBallPrefab;


    Animator enemyAnimator;
    private void Start()
    {
        enemyAnimator = GetComponent<Animator>();
    }
    void Update()
    {
        if (enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Claw Attack"))
        {
            shootFireBall();
        }
    }

    void shootFireBall()
    {
        Instantiate(fireBallPrefab, hitPoint.position, Quaternion.identity);
    }
}
