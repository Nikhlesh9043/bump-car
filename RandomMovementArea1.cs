using System.Collections;

    public int maxHealth = 5;
        {
            distance = Vector3.Distance(transform.position, target.transform.position);

            if (distance < 0f)
            {
                currentPoint = Random.Range(0, GameplayManager.instance.randomPoints.Length);
                target = GameplayManager.instance.randomPoints[currentPoint];
                agent.SetDestination(target.transform.position);
            }


            if (!agent.hasPath)
            {
                currentPoint = Random.Range(0, GameplayManager.instance.randomPoints.Length);
                target = GameplayManager.instance.randomPoints[currentPoint];
                agent.SetDestination(target.transform.position);
            }


    void takeDamage(int damage, GameObject player)
    {
        gameObject.SetActive(true);
        currentPoint = Random.Range(0, GameplayManager.instance.randomPoints.Length );
        agent.SetDestination(target.transform.position);

        currentHealth = maxHealth;
    }