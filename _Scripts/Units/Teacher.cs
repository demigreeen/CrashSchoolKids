using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;
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
    [SerializeField] private State DoNothingState;

    [HideInInspector] public string patrolStateName;
    [HideInInspector] public string goToDropedItemStateName;
    [HideInInspector] public string stayStateName;
    [HideInInspector] public string lookAroundStateName;
    [HideInInspector] public string angryStateName;
    [HideInInspector] public string angryStayStateName;
    [HideInInspector] public string DoNothingStateName;

    [Space(20)]
    [SerializeField] private DragAndDropItem playerDragAndDrop;
    [SerializeField] private PushItem pushItem;
    [SerializeField] private ProgressBar progressBar;
    [SerializeField] private CutsceneManager cutsceneManager;
    [Space(20)]
    [Header("Audio")]
    [SerializeField] public AudioSource walkAudio;
    [SerializeField] public AudioSource angryMusic;
    public AudioSource basicMusic;


    [HideInInspector] public GameObject angryToUnit;

    private NavMeshAgent agent;

   
    public override void Start()
    {
        base.Start();

        agent = GetComponent<NavMeshAgent>();

        playerDragAndDrop.ItemDroped += GoToDropedItem;
        pushItem.ItemDroped += GoToDropedItem;

        if (basicMusic.volume == 0)
        {
            angryMusic.volume = 0;
        }
        if (basicMusic.isPlaying == false)
        {
            basicMusic.Play();
        }
    
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
                if (angryMusic.isPlaying == true)
                {
                    angryMusic.Stop();
                }
                if (basicMusic.isPlaying == false)
                {
                    basicMusic.Play();
                }
            }
        }

        if (currentState.name == patrolStateName && agent.speed != 0)
        {
            if (walkAudio.isPlaying == false)
            {
                walkAudio.Play();
            }
        }
        else
        {
            if (walkAudio.isPlaying == true)
            {
                walkAudio.Stop();
            }
        }
    }

    public void GoToDropedItem()
    {
        SetState(GoToDropedItemState, ref goToDropedItemStateName);
        if (basicMusic.isPlaying == true)
        {
            basicMusic.Stop();
        }
        if (angryMusic.isPlaying == false)
        {
            angryMusic.Play();
        }
    }
    public void GoPatrolState()
    {
        SetState(PatrolState, ref patrolStateName);
        if (angryMusic.isPlaying == true)
        {
            angryMusic.Stop();
        }
        if (basicMusic.isPlaying == false)
        {
            basicMusic.Play();
        }
    }
    public void GoDoNothingState()
    {
        SetState(DoNothingState, ref DoNothingStateName);
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
            bool isEnd = progressBar.DoIconStep(angryToUnit);
            SetState(PatrolState, ref patrolStateName);
            if (PlayerPrefs.GetInt("mode") == 1)
                isEnd = false;

            if (isEnd == false)
            {
                cutsceneManager.StartCutscene(angryToUnit);
            }
            else
            {
                cutsceneManager.StartGameOvetCutscene(angryToUnit.name);
            }
            angryToUnit = null;
        }
        else
        {
            SetState(PatrolState, ref patrolStateName);
            if (angryMusic.isPlaying == true)
            {
                angryMusic.Stop();
            }
            if (basicMusic.isPlaying == false)
            {
                basicMusic.Play();
            }
        }
    }

}
