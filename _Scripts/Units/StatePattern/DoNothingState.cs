using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu]
public class DoNothingState : State
{
    public float time;

    public override void Run()
    {
        if (time <= 0)
        {
            unit.GetComponent<NavMeshAgent>().enabled = true;
            isFinished = true;
        }
        else
        {
            time -= Time.deltaTime;
        }
    }
}
