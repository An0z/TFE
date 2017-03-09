using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenshake : MonoBehaviour
{

    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    public Transform camTransform;

    // How long the object should shake for.
    public float shakeTimer = 0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    public Vector3 originalPos;

    void Awake()
    {
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }
    /*
    void OnEnable()
    {
        originalPos = camTransform.localPosition;
    }
    */
    void Update()
    {
        if (shakeTimer > 0)
        {
            //GetComponent<myCam>().enabled = false;
            //camTransform.localPosition = camTransform.position + Random.insideUnitSphere * shakeAmount;
            //camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            camTransform.position = FindObjectOfType<myCam>().transform.position + Random.insideUnitSphere * shakeAmount;
            //camTransform.position = GetComponent<myCam>().camPos + Random.insideUnitSphere * shakeAmount;
            shakeTimer -= Time.deltaTime * 1;
            shakeAmount =  shakeTimer/3;
            //GetComponent<myCam>().enabled = true;
            originalPos = GetComponent<myCam>().transform.position;

            //originalPos = camTransform.localPosition;
        }
        else
        {
            //GetComponent<myCam>().enabled = true;
            originalPos = FindObjectOfType<myCam>().transform.position;
            //originalPos = GetComponent<myCam>().camPos;
            shakeTimer = 0f;
            shakeAmount = 0f;
        }
    }
public void Shake(float time)
    {
        shakeTimer = time;
    }
}
