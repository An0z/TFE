using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class myCharacter : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float crouchSpeed = 3f;
    public float changeSpeed = 0.25f;

    public float HSpeed = 0f;
    public float VSpeed = 0f;
    public int moveRight = 1;
    public int moveTop = 0;
    public Vector3 carryValue = new Vector3(0.5f, 0.3f, -0.2f);
    public Vector3 carry;
    public bool collided = false;
    public string item;
    public GameObject itemObject;
    public bool isHiding = false;
    public bool boolCarry = false;
    public bool isCarrying = false;
    Animator mAnim;

    public myFollower daughter;

    public string chatText;
    public string[] arrestedList;
    public int tiredCounter;
    public int arrestedCount = 0;
    public bool arrested = false;
    public GameObject soldierArrest;

    public bool isRunning = false;


    // Use this for initialization
    void Start()
    {
        mAnim = GetComponent<Animator>();
        carry = new Vector3(this.transform.position.x + carryValue.x, this.transform.position.y + carryValue.y, this.transform.position.z + carryValue.z);
    }

    void Update()
    {
        mAnim.speed = HSpeed / walkSpeed;

        if (isRunning)
            run();
        else if (isHiding)
            crouch();
        else
            walk();
    }

    public void isMoving(bool move)
    {
        mAnim.SetBool("isIdling", !move);
    }

    public void move(Vector3 movement)
    {
        Vector3 oldPos = transform.position;
        GetComponent<Rigidbody>().velocity = movement;
        if (moveTop == 0)
        {
            mAnim.SetInteger("isFront", 0);
        }
        else if (moveTop == 1)
        {
            mAnim.SetInteger("isFront", 2);
        }
        else if (moveTop == 2)
        {
            mAnim.SetInteger("isFront", 1);
        }
        if (moveRight == 0)
        {
            mAnim.SetInteger("Righted", 0);
        }
        else if (moveRight == 1)
        {
            mAnim.SetInteger("Righted", 1);
        }
        else if (moveRight == 2)
        {
            mAnim.SetInteger("Righted", 2);
        }


        if (moveTop != 0 && moveRight == 0) //Vertical movement Only
        {
            carry = new Vector3(this.transform.position.x + carryValue.x, this.transform.position.y + carryValue.y, this.transform.position.z + carryValue.z);
        }

        if (moveRight != 0) //Horizontal movements
        {
            mAnim.SetInteger("isFront", moveTop);
            if (moveRight == 1)
            {
                carry = new Vector3(this.transform.position.x + carryValue.x, this.transform.position.y + carryValue.y, this.transform.position.z + carryValue.z);
            }
            else if (moveRight == 2)
            {
                carry = new Vector3(this.transform.position.x + carryValue.x, this.transform.position.y + carryValue.y, this.transform.position.z + carryValue.z);
            }
        }


    }
   
    void OnTriggerEnter(Collider col)
    {
        //Debug.Log("tag:" + col.gameObject.tag);///////////////////////////////////////////////////////////////////////////////////////////////////////////
        if (col.gameObject.name == "Daughter")
        {
            chatText = "Your Daughter";
            boolCarry = true;
            //Debug.Log("You can't stack your daughter !");////////////////////////////////////////////////////////////////////////////////////////////////
            
        }
        //**********************************// DEBUG COLLISION //************************************************************************************//
        else if(col.gameObject.tag=="debug")
        {
            chatText = col.GetComponent<soldierBehaviour>().dialog;
        }
        //******************************************************************************************************************************************//
        else if(col.gameObject.tag == "wall")
        {
            isHiding = true;
        }
        else if(col.gameObject.tag== "soldier" && isHiding == false)
        {
            soldierArrest = col.gameObject;
            chatText = col.GetComponent<soldierBehaviour>().dialog;
            arrestedCount = col.GetComponent<soldierBehaviour>().nbItemsAsked;
            //  Debug.Log("GetArrested !"); ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            arrestedList = col.GetComponent<soldierBehaviour>().itemAsked;
            arrested = true;
            col.gameObject.GetComponent<BoxCollider>().enabled = false;
            col.gameObject.SendMessage("Stop");
        }
        else
        {
            collided = true;
            itemObject = col.gameObject;
            item = col.gameObject.name;
            //Debug.Log("EnterCollider: " + item + "..........");//////////////////////////////////////////////////////////////////////////////////////////////
        }
    }

    public void endArrest()
    {
        soldierArrest.SendMessage("Resume");
    }

    public void resetColl()
    {        
        collided = false;
        item = "";
    }

    void OnTriggerExit(Collider col)
    {
        chatText = "";
        if (col.gameObject.name == "Daughter")
        {
            boolCarry = false;
            //Debug.Log("You can't stack your daughter !");////////////////////////////////////////////////////////////////////////////////////////////////
        }

        if (col.gameObject.tag == "wall")
        {
            isHiding = false;
        }

        if(col.gameObject.tag=="soldier")
        {
            col.gameObject.SendMessage("Resume");
        }
        collided = false;
        item = "";
        //arrestedList = new string[0];
        //Debug.Log("ExitCollider");////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }

    public string interact()
    {
        //Debug.Log("interact: " + item+"..........");/////////////////////////////////////////////////////////////////////////////////////////////////
        if (collided)
        {
            //mAnim.SetBool("Pick", true);
            mAnim.SetTrigger("Pick");
            itemObject.SetActive(false);            
            return item;
        }
        item = "";
        return item;    
    }

    public void initRun()
    {
        isRunning = !isRunning;
    }

    public void run()
    {
        if(HSpeed<runSpeed)
        {
            StartCoroutine(RunUp());
        }
        else if(HSpeed >= runSpeed)
        {
            HSpeed = runSpeed;
            StopCoroutine(RunUp());
            mAnim.SetBool("isRunning", true);
        }
    }

    private IEnumerator RunUp()
    {
        //Debug.Log("START RUN UP");

        yield return new WaitForSeconds(0.5f);
        HSpeed += changeSpeed;
        if (HSpeed >= runSpeed)
        {
            HSpeed = runSpeed;
            StopCoroutine(RunUp());
        }
    }

    public void crouch()
    {
        if(HSpeed > crouchSpeed )
        {
            StartCoroutine(CrouchUp());
        }
        else if (HSpeed <= crouchSpeed)
        {
            StopCoroutine(CrouchUp());
            mAnim.SetBool("isHiding", true);
            daughter.mAnim.SetBool("isHiding", true);
        }
    }
    private IEnumerator CrouchUp()
    {
       // Debug.Log("START CROUCH UP");

        yield return new WaitForSeconds(0.5f);
        HSpeed -= changeSpeed;
        if (HSpeed <= crouchSpeed)
        {
            HSpeed = crouchSpeed;
            StopCoroutine(CrouchUp());
        }
    }

    public void walk()
    {
        if ((int)HSpeed == (int)walkSpeed)
        {
           // Debug.Log("WALKING");
            StopCoroutine(RunUp());
            StopCoroutine(WalkUp());
            StopCoroutine(WalkDown());
            mAnim.SetBool("isHiding", false);
            daughter.mAnim.SetBool("isHiding", false);
            mAnim.SetBool("isRunning", false);
            daughter.mAnim.SetBool("isRunning", false);

            HSpeed = walkSpeed;
        }
        else if (HSpeed < walkSpeed)
        {
            //Debug.Log("START WALK UP");
            StartCoroutine(WalkUp());
        }
        else if(HSpeed > walkSpeed)
        {
            //Debug.Log("START WALK DOWN");
            StartCoroutine(WalkDown());
        }
    }

    private IEnumerator WalkUp()
    {
        yield return new WaitForSeconds(0.5f);
        HSpeed += changeSpeed;
        if (HSpeed >= runSpeed)
        {
            HSpeed = runSpeed;
            StopCoroutine(WalkUp());
        }
    }

    private IEnumerator WalkDown()
    {
        yield return new WaitForSeconds(0.5f);
        HSpeed -= changeSpeed;
        if (HSpeed <= walkSpeed)
        {
            HSpeed = walkSpeed;
            StopCoroutine(WalkDown());
        }
    }

    public bool canCarry()
    {
        return boolCarry;
    }

    public void changeCarry()
    {
        isCarrying = !isCarrying;
        if (isCarrying)
            mAnim.SetTrigger("StartCarry");
        else
            mAnim.SetTrigger("StopCarry");
    }

    public void Inventory()
    {
        this.GetComponentInParent<myController>().Inventory();
    }

    public void Carry()
    {
        changeCarry();
        daughter.changeCarry();
    }

    public void Run()
    {
        initRun();
    }

    public void moreFog(float valueMax)
    {
        StartCoroutine(startMoreFog(valueMax));
    }

    public IEnumerator startMoreFog(float valueMax)
    {
        if (RenderSettings.fogDensity < valueMax)
        {
            Debug.Log("MORE FOG");
            yield return new WaitForSeconds(0.1f);
            RenderSettings.fogDensity += 0.001f;
            StartCoroutine(startMoreFog(valueMax));
        }
        else
        {
            Debug.Log("STOP MORE FOG");
            StopCoroutine(startMoreFog(valueMax));
        }

    }

    public void lessFog(float valueMin)
    {
        StartCoroutine(startLessFog(valueMin));
    }

    public IEnumerator startLessFog(float valueMin)
    {
        if (RenderSettings.fogDensity > valueMin)
        {
            Debug.Log("LESS FOG");
            yield return new WaitForSeconds(0.2f);
            RenderSettings.fogDensity -= 0.0005f;
            StartCoroutine(startLessFog(valueMin));
        }
        else
            StopCoroutine(startLessFog(valueMin));
    }
    /*
    public void stopFog()
    {
        Debug.Log("STOP FOG");
        StopCoroutine(startMoreFog());
        StopCoroutine(startLessFog());
    }*/

}
