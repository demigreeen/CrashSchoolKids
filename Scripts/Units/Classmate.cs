using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Classmate : Unit
{
    [Space(10)]
    [Header("Custom")]
    [SerializeField] private State patrolState;

    private string patrolStateName;

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
    }

}
