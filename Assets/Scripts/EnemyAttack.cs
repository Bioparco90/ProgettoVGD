using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    public Transform hitPoint;
    public float attackRange = 2.0f;
    public LayerMask playerLayer;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void Attack()
    {
        Collider[] hitPlayer = Physics.OverlapSphere(hitPoint.position, attackRange, playerLayer);
        Debug.Log(hitPlayer.Length);
        foreach (Collider player in hitPlayer)
        {
            Debug.Log("Colpito" + player.name);
            player.GetComponent<PlayerController>().takeDamage(10);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (hitPoint == null)
            return;
        Gizmos.DrawWireSphere(hitPoint.position, attackRange);
    }
}
