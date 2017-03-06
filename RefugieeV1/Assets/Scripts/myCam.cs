using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class myCam : MonoBehaviour
{

    public GameObject character;

    public float step = 0.5f;
    public float distCamMin = 1f;
    public float distCamChara;
    public float distCamMax = 2f;
    public float stopCamDist = 4f;
    public float distZoom = 5f;

    public bool Zoomed;

    public Vector3 camPos;
    public Vector3 initCam;
    // Use this for initialization
    void Start ()
    {
        Zoomed = false;
        distCamChara = distCamMin;
        initCam = transform.position;
        camPos = new Vector3(character.transform.position.x, initCam.y, character.transform.position.z-distCamChara);
        transform.position = camPos;
        
        //this.camera.
    }


    void Update()
    {
        if (!Zoomed)
        {
            if (character.transform.position.z >= stopCamDist)              //DEZZOOM
            {
                if (distCamChara < distCamMax)
                    distCamChara += step;
                else if (distCamChara > distCamMax)
                    distCamChara = distCamMax;
                camPos = new Vector3(character.transform.position.x, initCam.y, character.transform.position.z - distCamChara);
                transform.position = camPos;

            }
            else
            {
                if (distCamChara > distCamMin)
                    distCamChara -= step;
                if (distCamChara < distCamMin)
                    distCamChara += step;
                camPos = new Vector3(character.transform.position.x, initCam.y, character.transform.position.z - distCamChara);
                transform.position = camPos;

            }
        }

        if(Zoomed)
        {
            if (distCamChara >= distZoom)
                distCamChara -= step ;
            camPos = new Vector3(character.transform.position.x, initCam.y, character.transform.position.z - distCamChara);
            transform.position = camPos;

        }
    }

    public void pause(bool isPaused)
    {
        //print("Blur ON / OFF");
        this.GetComponent<Blur>().enabled = isPaused;
    }

    public void zoom()
    {
        Zoomed = !Zoomed;
    }

}
