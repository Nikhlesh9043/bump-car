using System.Collections;using System.Collections.Generic;using UnityEngine;using UnityEngine.AI;public class RandomMovementArea1 : MonoBehaviour{        private int currentPoint;    public Camera Cam;    private NavMeshAgent agent;    private GameObject target;    private float distance;

    public int maxHealth = 5;    public int currentHealth;    public HealthBar healthBar;    private void Start()    {        agent = this.GetComponent<NavMeshAgent>();        ResetCar();    }    // Update is called once per frame    private void Update()    {        if (!GameplayManager.instance.gameOver)
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
            }        }    }    void OnCollisionEnter(Collision col)    {        if (col.gameObject.tag == "Player")        {            var magnitude = 2000;            var force = col.transform.position - transform.position;            force.Normalize();            col.gameObject.GetComponent<Rigidbody>().AddForce(force * magnitude);            takeDamage(1, col.gameObject);        }    }


    void takeDamage(int damage, GameObject player)    {        currentHealth -= damage;        healthBar.SetHealth(currentHealth);        if (currentHealth < 1)        {            DestroyCar();            player.GetComponent<PlayerData>().score += 1;            GameplayManager.instance.SortPlayers();        }    }    private void DestroyCar()    {        gameObject.SetActive(false);        ExplosionManager.instance.CreateExplosion(gameObject.transform.position);        GameplayManager.instance.SpawnCivilCar(gameObject);    }    public void ResetCar()
    {
        gameObject.SetActive(true);
        currentPoint = Random.Range(0, GameplayManager.instance.randomPoints.Length );        target = GameplayManager.instance.randomPoints[currentPoint];
        agent.SetDestination(target.transform.position);

        currentHealth = maxHealth;        healthBar.SetHealth(maxHealth);
    }}