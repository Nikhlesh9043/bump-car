﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class io_movement : MonoBehaviour
{
    private NavMeshAgent NavM;

    private GameObject target;


    private void Start()

    }

    void Update()
    {
        FindClosestEnemy();
        agent.SetDestination(target.transform.position);
    }

    void FindClosestEnemy()
    {
        float distanceToClosestEnemy = Mathf.Infinity;
        GameObject closestEnemy = null;

        foreach (GameObject currentEnemy in GameplayManager.instance.civilCars)
        {
            float distanceToEnemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;
            if (distanceToEnemy < distanceToClosestEnemy)
            {
                distanceToClosestEnemy = distanceToEnemy;
                closestEnemy = currentEnemy;

            }

        }
        target = closestEnemy;
    }

}