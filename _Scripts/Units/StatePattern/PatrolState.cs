using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu]
public class PatrolState : State
{
    public int startPatrolArea;
    public float speed;
    public float minWaitTime;
    public float maxWaitTime;

    private NavMeshAgent agent;
    private GameObject currentPatrolArea;
    private List<Transform> currentPatrolPoints;
    private List<Transform> nearestPoints;
    private Transform currentPoint;
    private float waitTimer;
    private Animator animator;
    private bool isClassmate;

    public override void Init()
    {
        animator = unit.GetComponent<Animator>();
        agent = unit.GetComponent<NavMeshAgent>();
        agent.speed = 0;
        isClassmate = unit.GetComponent<Classmate>() != null;

        currentPatrolArea = GameObject.Find("Area" + startPatrolArea);
        currentPatrolPoints = new List<Transform>();

        Transform[] arrayPoints = currentPatrolArea.GetComponentsInChildren<Transform>();
        currentPoint = null;
        foreach (Transform point in arrayPoints)
        {
            if (point.name != "Area" + startPatrolArea)
            {
                currentPatrolPoints.Add(point);
            }
        }

        waitTimer = Random.Range(minWaitTime, maxWaitTime);

        ChooseNextPoint();

    }

    public override void Run()
    {
        if (isFinished == false && waitTimer <= 0)
        {
            if (TutorialManager.instance.isNotHide == false) { agent.speed = speed; }
            else { agent.speed = 0; }

            Move();
            
                if (animState != AnimState.Walk)
                {
                    animState = AnimState.Walk;
                }
            
        }
        else if(waitTimer > 0)
        {
            waitTimer -= Time.deltaTime;
           
                if (animState != AnimState.Stay)
                {
                    animState = AnimState.Stay;
                }
            
        }
    }

    void Move()
    {
        Vector3 targetPos = new Vector3(currentPoint.position.x, currentPoint.position.y, currentPoint.position.z);

        if (agent.enabled == true && agent.destination != targetPos && Vector3.Distance(unit.transform.position, targetPos) >=2)
        {
            agent.destination = targetPos;
        }
        else if (agent.enabled == true && Vector3.Distance(unit.transform.position, targetPos) <= 2 && isFinished == false)
        {
            agent.speed = 0;
            isFinished = true;
        }
    }

    void ChooseNextPoint()
    {
        currentPatrolPoints.Sort((a, b) => Vector3.Distance(unit.transform.position, a.position).CompareTo(Vector3.Distance(unit.transform.position, b.position)));
        nearestPoints = new List<Transform>();
        nearestPoints.Clear();
        if (currentPatrolPoints[0] != currentPoint)
        {
            nearestPoints.Add(currentPatrolPoints[0]);
        }
        if (currentPatrolPoints[1] != currentPoint)
        {
            nearestPoints.Add(currentPatrolPoints[1]);
        }
        if (currentPatrolPoints[2] != currentPoint)
        {
            nearestPoints.Add(currentPatrolPoints[2]);
        }
        if (currentPatrolPoints[3] != currentPoint)
        {
            nearestPoints.Add(currentPatrolPoints[3]);
        }
        if (currentPatrolPoints[4] != currentPoint)
        {
            nearestPoints.Add(currentPatrolPoints[4]);
        }

        currentPoint = nearestPoints[Random.Range(0, 4)];
    }

    

}
