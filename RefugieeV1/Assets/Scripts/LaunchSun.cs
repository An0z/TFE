using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchSun: MonoBehaviour
{

    [SerializeField]
    public GameObject SunLights;

    [SerializeField]
    private string myFunction;

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
        SendMessage(myFunction);
    }

    void OnTriggerEnter(Collider col)
    {
        //Debug.Log("Enter 2 !");
        SendMessage(myFunction);
    }

    private void SunDown()
    {
        SunLights.GetComponent<Animator>().SetTrigger("sunDown");
    }

    private void SunUp()
    {
        SunLights.GetComponent<Animator>().SetTrigger("sunUp");
    }
}
