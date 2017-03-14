using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkUINPC : MonoBehaviour
{
    [SerializeField]
    float Y;

    [SerializeField]
    Transform Icon;

    [SerializeField]
    Transform NPC;

    [SerializeField]
    myCharacter Player;

    // Use this for initialization
    void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        Icon.position = new Vector3(NPC.position.x, NPC.position.y + Y, NPC.position.z);
	}
}
