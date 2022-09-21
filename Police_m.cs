using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Police_m : MonoBehaviour
{
    public Camera Cam;
    public NavMeshAgent agent;
    private GameObject currentTarget;
    private float distance;
    private int currentPoint;


    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        FindClosestEnemy();

        distance = Vector3.Distance(transform.position, currentTarget.transform.position);
        if (distance < 15f)
        {

            agent.SetDestination(currentTarget.transform.position);

        }

        if (!agent.hasPath)
        {
            currentPoint = Random.Range(0, GameplayManager.instance.randomPoints.Length);
            currentTarget = GameplayManager.instance.randomPoints[currentPoint];
            agent.SetDestination(currentTarget.transform.position);
        }
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
         currentTarget = closestEnemy;
    }
}
