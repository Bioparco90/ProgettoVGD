using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class EnemyAttack : MonoBehaviour
{
    public Transform hitPoint;
    public float attackRange = 7.0f;
    public LayerMask playerLayer;

    void Attack()
    {
        StackTrace stackTrace = new StackTrace();           // get call stack
        StackFrame[] stackFrames = stackTrace.GetFrames();  // get method calls (frames)

        // write call stack method names
        foreach (StackFrame stackFrame in stackFrames)
        {
            print(stackFrame.GetMethod().CallingConvention);   // write method name
        }
        Collider[] hitPlayer = Physics.OverlapSphere(hitPoint.position, attackRange, playerLayer);
        //print(hitPlayer.Length);
        foreach (Collider player in hitPlayer)
        {
            //print("Colpito " + player.name);
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
