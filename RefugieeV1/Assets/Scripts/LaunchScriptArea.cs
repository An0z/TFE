using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchScriptArea : MonoBehaviour
{

    [SerializeField]
    private string myFunction;
    [SerializeField]
    private float value;
    
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
        Debug.Log("Enter !");
        if (value != 0)
            col.gameObject.SendMessage(myFunction, value);
        else
            col.gameObject.SendMessage(myFunction);
    }

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("Enter 2 !");
        if(value!=0)
        col.gameObject.SendMessage(myFunction,value);
        else
            col.gameObject.SendMessage(myFunction);
    }
}
