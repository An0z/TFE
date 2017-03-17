using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeThrough : MonoBehaviour
{
    [SerializeField]
    private Transform m_camera;

    GameObject invisible;

    public Color White;
    public Color Transparent;

    public Material house;
    public Material houseAlpha;

    // Use this for initialization
    void Start ()
    {
		
	}

    void FixedUpdate()
    {
        RaycastHit[] hits;

        hits = Physics.RaycastAll(m_camera.position, transform.position, 100f);
        if (hits.Length > 0)
        {
            Debug.DrawLine(m_camera.position, transform.position);
            foreach (RaycastHit hit in hits)
                if (hit.collider.gameObject.tag == "tohide")
                {
                    Debug.Log("There is something in front of the object!:" + hit.collider.name);
                    if (invisible != hit.collider.gameObject)
                    {
                        setInvisible(255f);
                        invisible = hit.collider.gameObject;
                        setInvisible(127.5f);
                    }                   
                }
        }
        else
        {
            setInvisible(255f);
            Debug.Log("Nothing in front");
        }
    }

    public void setInvisible(float alpha)
    {
        Debug.Log("Name of the object!:" + invisible.name);
        //invisible.GetComponent<Renderer>().material = new Material(invisible.GetComponent<Renderer>().material);
        //invisible.GetComponent<Renderer>().material.color = new Color(White.r, White.g, White.b, alpha);
        if (alpha == 127.5)
            invisible.GetComponent<Renderer>().material = house;
        //invisible.GetComponent<Renderer>().material.color = White;

        if (alpha == 255)
            invisible.GetComponent<Renderer>().material = houseAlpha;
        //invisible.GetComponent<Renderer>().material.color = Transparent;
    }


}
