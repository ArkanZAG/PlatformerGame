using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ArrowTrap : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [FormerlySerializedAs("fireballs")] [SerializeField] private GameObject[] arrow;
    private float cooldownTimer;

    private void Attack()
    {
        cooldownTimer = 0;

        arrow[FindArrow()].transform.position = firePoint.position;
        arrow[FindArrow()].GetComponent<EnemyProjectile>().ActivateProjectile();
    }

    private int FindArrow()
    {
        for (int i = 0; i < arrow.Length; i++)
        {
            if (!arrow[i].activeInHierarchy)
            {
                return i;
            }
        }

        return 0;
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        
        if (cooldownTimer >= attackCooldown)
        {
            Attack();
        }
    }
}
