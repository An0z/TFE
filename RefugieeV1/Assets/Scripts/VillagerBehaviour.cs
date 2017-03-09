using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerBehaviour : MonoBehaviour
{
    public float randomDeath = 5;
    public bool move = false;
    public float speed;
    public Transform goPoint;
    UnityEngine.AI.NavMeshAgent agent;


    // Use this for initialization
    void Start ()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        GetComponent<Animator>().speed = speed;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(move)
        {
            //transform.position += new Vector3(speed, 0, 0);

            agent.destination = goPoint.position;

            if (Random.Range(0, 10) > randomDeath && move)
            {
                agent.Stop();
                fall();
            }
        }
	}

    public void run()
    {
        move = true;
        GetComponent<Animator>().SetBool("run", true);
    }

    public void fall()
    {
        move = false;
        GetComponent<Animator>().SetBool("fall", true);
        GetComponent<Animator>().SetBool("run", false);
    }

}
