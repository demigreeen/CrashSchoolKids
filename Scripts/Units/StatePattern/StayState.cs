using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu]
public class StayState : State
{
    public float stayTime;

    private NavMeshAgent agent;
    private Animator animator;
    public override void Init()
    {
        animator = unit.GetComponent<Animator>();
        agent = unit.GetComponent<NavMeshAgent>();

    }


    public override void Run()
    {
        stayTime -= Time.deltaTime;
        if (stayTime <= 0)
        {
            isFinished = true;
        }
        else
        {
            agent.speed = 0;
            animState = AnimState.Stay;
        }
    }
}
