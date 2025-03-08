using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static State;
using UnityEngine.AI;
using System.Runtime.InteropServices;
[CreateAssetMenu]
public class AngryState : State
{
    public float minDistance;
    public float angryTime;
    [Space(10)]
    public float speed;

    private float offsetUpdate;
    private Animator animator;
    private NavMeshAgent agent;

    private Transform modelTeacher;
    private Transform pointEye1;
    private Transform pointEye2;

    private Transform targerPos;

    public override void Init()
    {
        if (unit.GetComponent<Teacher>() != null)
        {
            targerPos = unit.GetComponent<Teacher>().angryToUnit.transform;
        }
        else
        {
            isFinished = true;
        }
        if (PlayerPrefs.GetInt("mode") == 1)
        {
            switch (PlayerPrefs.GetInt("difficult"))
            {
                case 0:
                    speed = speed - 0.5f;
                    break;
                case 1:
                    speed = speed + 0.5f;
                    break;
                case 2:
                    speed = speed + 1f;
                    break;
                case 3:
                    speed = speed + 2f;
                    break;
                default:
                    break;
            }
        }


        animator = unit.GetComponent<Animator>();
        agent = unit.GetComponent<NavMeshAgent>();
        agent.speed = speed;

       
    }

    public override void Run()
    {
        Move();
        CheckDistanceAndTime();
        angryTime -= Time.deltaTime;
        animState = AnimState.Run;
    }

    void CheckDistanceAndTime()
    {
        if ( angryTime <= 0 )
        {
            isFinished = true;
        }
        if (Vector3.Distance(unit.transform.position, targerPos.transform.position) < minDistance)
        {
            unit.GetComponent<Teacher>().GameOver();
        }
    }

    void Move()
    {
        agent.destination = targerPos.position;
        agent.speed = speed;
    }
}
