using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class police_movement : MonoBehaviour
{
   
    public NavMeshAgent agent;

    private GameObject target;


    private void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();

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

        foreach (GameObject currentEnemy in GameplayManager.instance.players)
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
