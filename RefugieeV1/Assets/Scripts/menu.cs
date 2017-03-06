using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {		
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Application.LoadLevel("MainMenu");
        }
    }

    public void StartGraphicDemo()
    {
        Application.LoadLevel("Level1GraphicComp");
    }

    public void StartTechnicalDemo()
    {
        Application.LoadLevel("Level1ReworkedCOMPOSIT-08-07-17");
    }

    public void Exit()
    {
        Application.Quit();
    }
    
}
