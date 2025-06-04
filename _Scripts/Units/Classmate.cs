using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Classmate : Unit
{
    [SerializeField] private Teacher teacher;

     [Space(10)]
    [Header("Custom")]
    [SerializeField] private State patrolState;
    [SerializeField] private State shockedState;
    [SerializeField] private State DoNothingState;


    [HideInInspector] public string patrolStateName;
    [HideInInspector] public string shockedStateName;
    [HideInInspector] public string DoNothingStateName;

    public override void Start()
    {
        base.Start();
    }
    public override void Update()
    {
        base.Update();

        if (currentState.isFinished == true)
        {
            if (true)
            {
                SetState(patrolState, ref patrolStateName);
            }
        }

        if (currentState.name == shockedStateName && teacher.currentState.name != teacher.goToDropedItemStateName && teacher.currentState.name != teacher.lookAroundStateName && teacher.currentState.name != teacher.angryStateName)
        {
            SetState(patrolState, ref patrolStateName);
        }
    }
    public void GoDoNothingState()
    {
        SetState(DoNothingState, ref DoNothingStateName);
    }
    public void Shocked()
    {
        SetState(shockedState, ref shockedStateName);
    }

}
