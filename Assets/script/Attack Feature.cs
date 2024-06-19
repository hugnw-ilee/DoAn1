using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackFeature : MonoBehaviour
{
    public float attackRange;

    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
