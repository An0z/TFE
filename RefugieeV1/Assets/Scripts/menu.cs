using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu : MonoBehaviour
{

    public string[] Button;

	// Use this for initialization
	void Start ()
    {		
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Application.LoadLevel(Button[0]);
        }
    }

    public void StartGraphicDemo()
    {
        Application.LoadLevel(Button[1]);
    }

    public void StartTechnicalDemo()
    {
        Application.LoadLevel(Button[2]);
    }

    public void Exit()
    {
        Application.Quit();
    }
    
}
