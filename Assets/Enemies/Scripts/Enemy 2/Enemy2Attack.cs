using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Attack : MonoBehaviour
{
    public Transform hitPoint;
    public GameObject fireBallPrefab;
    float timer;


    Animator enemyAnimator;
    private void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        timer = 0;
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Claw Attack") && timer >= 1)
        {
            shootFireBall();
            timer = 0;
        }
    }

    void shootFireBall()
    {
        Instantiate(fireBallPrefab, hitPoint.position, Quaternion.identity);
    }
}
