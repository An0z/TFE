using UnityEngine;
using System.Collections;

public class Navigation : MonoBehaviour
{

    public Transform[] goals;
    public Transform actualGoal;
    public Transform firstGoal;

    public float minSpeed = 2f;
    public float maxSpeed = 4f;
    UnityEngine.AI.NavMeshAgent agent;
    int goalNum;
    public float distance = 0.5f;
    public float rotationSpeed = 1f;
    float rotationZ;
    float timer;
    public float wait = 3f;
    bool Bwait = false;

    public bool stopped;

    void Start()
    {
        stopped = false;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

            agent.speed = Random.Range(minSpeed,maxSpeed);
        
        goalNum = 0;
        agent.destination = firstGoal.position;
        actualGoal = firstGoal;
    }



    // Update is called once per frame
    void Update()
    {
        if (!stopped)
        {
            agent.Resume();
            if (Vector3.Distance(goals[goalNum].position, agent.transform.position) < distance)
            {
                goalNum = goalNum + 1;
                if (goalNum == goals.Length)
                    goalNum = 0;
                agent.destination = goals[goalNum].position;
                actualGoal = goals[goalNum];
            }
        }
        if (stopped && Bwait)
        {
            if (timer +wait < Time.realtimeSinceStartup)
            {
                stopped = false;
                Bwait = false;
            }
        }
    }

    public void Stop()
    {
        stopped = true;
        agent.Stop();
    }

    public void Resume()
    {
        Bwait = true;
        timer = Time.realtimeSinceStartup;    
    }
}
