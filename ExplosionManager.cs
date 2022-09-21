using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionManager : MonoBehaviour
{
    public static ExplosionManager instance;

    public GameObject ExplosionParticle;
    private GameObject currentCube;

    private List<GameObject> activeCubes;
    private float timePeriod;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        activeCubes = new List<GameObject>();
        timePeriod = 4;
    }

    // Update is called once per frame
    void Update()
    {
        if (activeCubes.Count > 0) {
            timePeriod -= Time.deltaTime;
            if (timePeriod < 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    Destroy(activeCubes[0]);
                    activeCubes.RemoveAt(0);
                }

                timePeriod = 1;
            }
        }
    }

    public void CreateExplosion(Vector3 target)
    {

        for(int i = 0; i < 10; i++)
        {
            currentCube = Instantiate(ExplosionParticle);
            currentCube.transform.localScale = Random.Range(0.2f, 0.6f) * Vector3.one;
            currentCube.transform.position = target;

            activeCubes.Add(currentCube);
        }
    }
}
