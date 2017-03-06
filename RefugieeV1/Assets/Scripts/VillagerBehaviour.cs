using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerBehaviour : MonoBehaviour
{

    public bool move = false;
    public float speed;

	// Use this for initialization
	void Start ()
    {
        GetComponent<Animator>().speed = speed;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(move)
        {
            transform.position += new Vector3(speed, 0, 0);

            if (Random.Range(0, 5) > 2)
                fall();
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
