using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_follow : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.z>71f)
        {
            gameObject.transform.position = player.transform.position + new Vector3(0, 16f, 20 - (player.transform.position.z - 72));
        }
        else
        {
            gameObject.transform.position = player.transform.position + new Vector3(0, 16f, 20);
        }
    }
}
