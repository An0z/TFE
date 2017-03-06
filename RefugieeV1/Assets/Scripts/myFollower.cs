using UnityEngine;
using System.Collections;

public class myFollower : MonoBehaviour
{
    public myCharacter character;
    public Animator mAnim;
    public float speed;
    public bool carry = false;

    public int tiredCounter;

    public Vector3 GroundPos;
    
    public void Start()
    {
        mAnim = GetComponent<Animator>();
    }

    public void Update()
    {
        // mAnim.speed = speed;

        if (speed < 2.6)
            mAnim.SetBool("isIdling", true);
        else
            mAnim.SetBool("isIdling", false);

    }

    public void move(Vector3 carryPos, Vector3 movement,float inverseX, int inverseZ)
    {
        if (carry)
        {
            this.transform.position = carryPos;
            GetComponent<Rigidbody>().velocity = movement;
        }
        else
        {
            this.transform.position = new Vector3(transform.position.x+inverseX*GroundPos.x, GroundPos.y, character.transform.position.z+inverseZ*GroundPos.z);
            speed = Vector3.Distance(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(character.transform.position.x, transform.position.y, character.transform.position.z));
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(character.transform.position.x+inverseX*GroundPos.x, transform.position.y, character.transform.position.z + inverseZ*GroundPos.z), step);
        }

        if (inverseZ == 0)//Horizontal, should check distance
        {
            if (inverseX > 0) //MOVE RIGHT
            {
                mAnim.SetInteger("isFront", 0);
                if (transform.position.x > character.transform.position.x + inverseX * GroundPos.x) //Anim move LEFT
                    mAnim.SetInteger("Righted", 2);
                else
                    mAnim.SetInteger("Righted", 1);
            }
            else if (inverseX < 0) // MOVE LEFT
            {
                if (transform.position.x < character.transform.position.x + inverseX * GroundPos.x) //Anim move RIGHT
                    mAnim.SetInteger("Righted", 1);
                else
                    mAnim.SetInteger("Righted", 2);

            }
        }
        else if (inverseZ > 0)  //TOP
        {
            mAnim.SetInteger("isFront", 1);
            mAnim.SetInteger("Righted", 0);
        }
        else if (inverseZ < 0)    // BACK
        {
            mAnim.SetInteger("isFront", 2);
            mAnim.SetInteger("Righted", 0);
        }

    }

    public void changeCarry()
    {
        this.carry = !this.carry;
    }

}
