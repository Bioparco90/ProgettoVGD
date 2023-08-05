using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Attack : MonoBehaviour
{
    public Transform hitPoint;
    public GameObject fireBallPrefab;

    void Attack()
    {
        Instantiate(fireBallPrefab, hitPoint.position, Quaternion.identity);
    }
}
