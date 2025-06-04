using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [Header("Main")]
    public State startState;
    public State currentState;
    public Transform currentPoint;
    public Transform lastCurrentPoint;

    public virtual void Start()
    {
        currentState = startState;
        currentState.unit = this;
        currentState.Init();
    }

    public virtual void Update()
    {
        currentState.Run();
    }

    public virtual void SetState(State state, ref string name) 
    {
        currentState = Instantiate(state);
        currentState.unit = this;
        name = currentState.name;
        currentState.Init();
    }
}
