using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class State : ScriptableObject
{
    public bool isFinished { get; protected set; }
    public AnimState animState { get { return (AnimState)unit.GetComponent<Animator>().GetInteger("States"); } set { unit.GetComponent<Animator>().SetInteger("States", (int)value); } }
    [HideInInspector] public Unit unit;

    public virtual void Init() { }
    public abstract void Run();

    public enum AnimState
    {
        Stay,
        Walk,
        Run,
        LookAround,
        AngryStay,
        Sit,
        Shocked
    }
    
}
