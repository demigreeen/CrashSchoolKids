using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teacher : Unit
{
    [Space(10)]
    [Header("Custom")]
    [SerializeField] private State PatrolState;
    [SerializeField] private State GoToDropedItemState;
    [SerializeField] private State StayState;
    [SerializeField] private State AngryStayState;
    [SerializeField] private State LookAroundState;
    [SerializeField] private State AngryState;

    private string patrolStateName;
    private string goToDropedItemStateName;
    private string stayStateName;
    private string lookAroundStateName;
    [HideInInspector] public string angryStateName;
    private string angryStayStateName;

    [Space(20)]
    [SerializeField] private DragAndDropItem playerDragAndDrop;
    [SerializeField] private ProgressBar progressBar;
    [SerializeField] private CutsceneManager cutsceneManager;


   [HideInInspector] public GameObject angryToUnit;

   
    public override void Start()
    {
        base.Start();

        playerDragAndDrop.ItemDroped += GoToDropedItem;
    }
    public override void Update()
    {
        base.Update();

        if (currentState.isFinished == true)
        {
            if (currentState.name == goToDropedItemStateName)
            {
                SetState(LookAroundState, ref lookAroundStateName);
            }
            else if (currentState.name == lookAroundStateName)
            {
                SetState(AngryStayState, ref angryStayStateName);
            }
            else
            {
                SetState(PatrolState, ref patrolStateName);
            }
        }
    }

    public void GoToDropedItem()
    {
        SetState(GoToDropedItemState, ref goToDropedItemStateName);
    }
    public void SomeoneInSight(GameObject angryToGo)
    {
        angryToUnit = angryToGo;
        SetState(AngryState, ref angryStateName);
    }

    public void GameOver()
    {
        if (angryToUnit != null)
        {
            progressBar.DoIconStep(angryToUnit);
            SetState(PatrolState, ref patrolStateName);
            cutsceneManager.StartCutscene(angryToUnit);
            angryToUnit = null;
        }
        else
        {
            SetState(PatrolState, ref patrolStateName);
        }
    }

}
