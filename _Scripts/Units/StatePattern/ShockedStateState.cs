using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu]
public class ShockedState : State
{

    private NavMeshAgent agent;
    private Animator animator;
    public override void Init()
    {
        animator = unit.GetComponent<Animator>();
        agent = unit.GetComponent<NavMeshAgent>();
        agent.enabled = true;
        CutsceneManager.instance.cutsceneEnd +=  () => isFinished = true;  
    }


    public override void Run()
    {

        agent.speed = 0f;
        animState = AnimState.Shocked;
        
    }
}
