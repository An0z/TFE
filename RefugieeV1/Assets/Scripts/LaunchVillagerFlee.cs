using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchVillagerFlee: MonoBehaviour
{

    [SerializeField]
    public VillagerBehaviour[] runningVillagers; 

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnCollisionEnter(Collision col)
    {
        //Debug.Log("Enter !");
        foreach (VillagerBehaviour vB in runningVillagers)
            vB.run();
        
    }

    void OnTriggerEnter(Collider col)
    {
        //Debug.Log("Enter 2 !");
        foreach (VillagerBehaviour vB in runningVillagers)
            vB.run();
    }
}
