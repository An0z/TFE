using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(myCharacter))]
[RequireComponent(typeof(myCam))]
[RequireComponent(typeof(myFollower))]
public class myController : MonoBehaviour
{
    public Inventory inv;
    public Text statut;
    public Text chat;
    public myCharacter mChara;
    public myFollower mGirl;
    public myCam mCam;

    public int controlTimer =0;
    public int waitTime = 50;

    public bool isPaused;

    // Use this for initialization
    void Start()
    {
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
       // if (mChara.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("fatherStartCarryLeft") || mChara.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("fatherStopCarryLeft") || mChara.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("fatherStartCarryRight") || mChara.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("fatherStopCarryRight") || mChara.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("fatherStopInventoryLeft - GIVEITEM") || mChara.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("fatherStopInventoryRight - GIVEITEM") || mChara.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("fatherStartInventoryLeft") || mChara.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("fatherStartInventoryRight") || mChara.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("fatherStopInventoryRight - CLOSEBAG") || mChara.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("fatherStopInventoryLeft - CLOSEBAG"))
        if (mChara.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("fatherStartCarryLeft") || mChara.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("fatherStopCarryLeft") || mChara.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("fatherStartCarryRight") || mChara.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("fatherStopCarryRight"))
        {
            isPaused = true;
            mGirl.GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            if(mGirl.GetComponent<SpriteRenderer>().enabled == false)
                isPaused = false;
            mGirl.GetComponent<SpriteRenderer>().enabled = true;
        }


        chat.text = mChara.chatText;
        if (mChara.arrested)                                                    //ARRESTATION - OPEN INVENTORY/CLOSE
        {
            mChara.isRunning = false;                                  //Reset the run bool -> you cannot run while being arrested
            if (mChara.arrestedCount > 0)
            {
                // Debug.Log("Verification arrested "); ////////////////////////////////////////////////////////////////////////                
                isPaused = true;
                mCam.pause(isPaused);
                inv.openClose(isPaused);
                mChara.GetComponent<Animator>().SetBool("Inventory", true);
                mChara.GetComponent<Animator>().SetBool("giveItem", false);
            }
            else
            {
                mChara.arrested = false;
                // Debug.Log("Enough !");   ////////////////////////////////////////////////////////////////////////////////////
                isPaused = false;
                mCam.pause(isPaused);
                inv.openClose(isPaused);
                mChara.GetComponent<Animator>().SetBool("Inventory", false);
            }
        }

        if (!isPaused)                                           //Not in the bag - Movement & actions
        { 
            if (mChara.isHiding)
                statut.text = "Hidden";
            else
                statut.text = "";

            if (Input.GetKeyDown("space") || Input.GetButtonDown("Carry"))
            {
                //  Debug.Log("SPACE \n");  ////////////////////////////////////////////////////////////////////////////////////
                if (mChara.canCarry())
                {
                    mChara.changeCarry();
                    mGirl.changeCarry();
                }
            }
            if(Input.GetKeyDown("return") || Input.GetButtonDown("Submit"))
            {
                  Debug.Log("SUBMIT BUTTON \n"); ////////////////////////////////////////////////////////////////////////////////////
                try { inv.addItem(mChara.interact());
                    mChara.resetColl();
                    //Debug.Log("MouseClickAdditem");////////////////////////////////////////////////////////////////////////////

                }
                catch { Debug.Log("No Item on collision"); }
            } 
            if(Input.GetKeyDown(KeyCode.LeftShift) || Input.GetButtonDown("Run"))
            {
                Debug.Log("RUN BUTTON \n"); ////////////////////////////////////////////////////////////////////////////////////
                mChara.initRun();
            }           
        }        
        else                                                   //In the bag - look at items AND loot
        {
            if (Input.GetKeyDown("return") || Input.GetButtonDown("Submit"))
            {
                Debug.Log("SUBMIT BUTTON \n"); ////////////////////////////////////////////////////////////////////////////////////
                mChara.arrestedCount-=inv.removeItem(mChara.arrestedList);
                mChara.GetComponent<Animator>().SetBool("giveItem", true);
                if (mChara.arrestedCount == 0)
                    mChara.endArrest();

            }
        }

        if ((Input.GetKeyDown(KeyCode.I) || Input.GetButtonDown("Inventory")) && !mChara.arrested)    //FREELY OPEN/CLOSE BAG
        {
            Debug.Log("INVENTORY BUTTON \n"); ////////////////////////////////////////////////////////////////////////////////////
            //Debug.Log("Open/Close bag");  /////////////////////////////////////////////////////////////////////////////////
            mChara.arrestedList = new string[0];
            isPaused = !isPaused;
            mCam.pause(isPaused);
            inv.openClose(isPaused);
            mChara.GetComponent<Animator>().SetBool("Inventory", isPaused);
        }
    }

    void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        //Debug.Log("MOVE X : " + moveX + "----------- MOVE Y : " + moveY);////////////////////////////////////////////////////////////////
        if (!isPaused)
        {
            if (moveX == 0 && moveY == 0) // no axis pressed, the character don't move
                mChara.isMoving(false);
            else
            {
                mChara.isMoving(true);
                if (moveY == 0)
                    mChara.moveTop = 0;
                else if (moveY > 0)
                    mChara.moveTop = 1;
                else if (moveY < 0)
                    mChara.moveTop = 2;

                if (moveX == 0)
                    mChara.moveRight = 0;
                else if (moveX > 0)
                    mChara.moveRight = 1;
                else if (moveX < 0)
                    mChara.moveRight = 2;
            }
            Vector3 myMove = new Vector3(moveX * mChara.HSpeed, 0, moveY * mChara.VSpeed / 2);

            if (moveX != 0 && moveY != 0)
            {
                //Debug.Log("Normalize direction");//////////////////////////////////////////////////////////////////////////////////////////
                myMove = new Vector3(moveX * mChara.HSpeed / 2, 0, (moveY * mChara.VSpeed) / 2);
            }

            mChara.move(myMove);

            if (mChara.moveRight == 1)
            {
                mGirl.move(mChara.carry, myMove, 1, 0);
            }
            else if (mChara.moveRight == 2)
            {
                mGirl.move(mChara.carry, myMove, -1, 0);
            }
            else if (mChara.moveRight == 0)
            {
                if (mChara.moveTop == 1)
                {
                    mGirl.move(mChara.carry, myMove, 0.5f, -1);
                }
                else if (mChara.moveTop == 2)
                {
                    mGirl.move(mChara.carry, myMove, -0.5f, 1);
                }
            }
            //mGirl.transform.position = (mChara.carry);
        }
        else //isPaused
        {
            mGirl.move(mChara.carry, new Vector3(0, 0, 0), 0, 0);
            mChara.GetComponent<Animator>().SetBool("isIdling", true);
            if (moveY > 0 && controlTimer<=0)
            {
                //Debug.Log("UP");  ////////////////////////////////////////////////////////////////////////////////////
                inv.movePointer(true);
                controlTimer = waitTime;
            }
            else if (moveY < 0 && controlTimer <= 0)
            {
                //  Debug.Log("DOWN");  ////////////////////////////////////////////////////////////////////////////////////
                inv.movePointer(false);
                controlTimer = waitTime;
            }
            controlTimer--;
        }

    }

    public void Inventory()
    {
        isPaused = true;
        mCam.pause(isPaused);
        inv.openClose(isPaused);
        mChara.GetComponent<Animator>().SetBool("Inventory", true);
        mChara.GetComponent<Animator>().SetBool("giveItem", false);
    }

    public void Shake(Vector3 bombPos)
    {
        if (Vector3.Distance(bombPos, mChara.transform.position) > 80 && Vector3.Distance(bombPos, mChara.transform.position) < 150)
        {
            mCam.GetComponent<Screenshake>().Shake(0.5f);
            StartCoroutine((Flash(0.5f)));
        }
        else if (Vector3.Distance(bombPos, mChara.transform.position) > 50 && Vector3.Distance(bombPos, mChara.transform.position) < 80)
        {
            mCam.GetComponent<Screenshake>().Shake(2);
            StartCoroutine((Flash(0.5f)));
        }
        else if (Vector3.Distance(bombPos, mChara.transform.position) < 5)
        {
            mCam.GetComponent<Screenshake>().Shake(3);
            StartCoroutine((Flash(0.5f)));
        }
    }
    private IEnumerator Flash(float timer)
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < timer*200; i++) ;
            RenderSettings.ambientIntensity += 0.5f;
        yield return new WaitForSeconds(timer);
        for (int i = 0; i < timer*200; i++) ;
        RenderSettings.ambientIntensity -= 0.5f;
    }
}
