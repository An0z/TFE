using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBombs : MonoBehaviour
{
    public float speed;
    public float[] speeds;

    ArrayList s = new ArrayList();
    public float initY;
    public float resetBombs = -5f;

    public bool shaking = false;

	// Use this for initialization
	void Start ()
    {
        speed = Random.Range(speeds[0], speeds[1]);
	}

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - speed, this.transform.position.z);
        
        if (this.transform.position.y < resetBombs + 1)
        {
            if (speed > 0.2f)
            {
                speed = speed / 2;
                if (shaking == false)
                {
                    FindObjectOfType<myController>().Shake(transform.position);
                    //GetComponent<myController>().Shake(transform.position);
                    shaking = true;
                }
            }
        }
            else if (this.transform.position.y < resetBombs + 3)
                if (speed > (speeds[0] / 8))
                       speed = speed / 1.2f;                    
                
        /*
        else if (this.transform.position.y < resetBombs + 10)
            speed = speed / 1.2f;*/

        if (this.transform.position.y <= resetBombs)
        {
            shaking = false;
            this.transform.position = new Vector3(this.transform.position.x, initY, this.transform.position.z);
            speed = Random.Range(speeds[0], speeds[1]);
        }
    }


}
