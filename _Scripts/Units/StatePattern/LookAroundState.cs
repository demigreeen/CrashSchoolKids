using System;
using System.Collections;
using System.Collections.Generic;
using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.UI.Image;

[CreateAssetMenu]
public class LookAroundState : State
{
    public float lookDistance;
    [Space(10)]
    public int repeatCount;

    private float currentRepeatCount;
    private float offsetUpdate;
    private Animator animator;
    private NavMeshAgent agent;

    private Transform modelTeacher;
    private Transform pointEye1;
    private Transform pointEye2;
    public override void Init()
    {
        animator = unit.GetComponent<Animator>();
        agent = unit.GetComponent<NavMeshAgent>();

        if (unit.GetComponent<Teacher>() != null)
        {
            modelTeacher = GameObject.Find("ModelTeacher").GetComponent<Transform>();
            pointEye1 = GameObject.Find("PointEye1").GetComponent<Transform>();
            pointEye2 = GameObject.Find("PointEye2").GetComponent<Transform>();
        }

    }
    public override void Run()
    {
        CheckLook();
        PlayAnimation();
    }

    void PlayAnimation()
    {
        if (currentRepeatCount < repeatCount && animator.GetCurrentAnimatorStateInfo(0).IsName("LookAround") == false && offsetUpdate <= 0)
        {
            animState = AnimState.LookAround;
            currentRepeatCount++;
            offsetUpdate = animator.GetCurrentAnimatorStateInfo(0).length;
        }
        else if (offsetUpdate > 0)
        {
            offsetUpdate -= Time.deltaTime;
        }
        else if (currentRepeatCount >= repeatCount)
        {
            isFinished = true;
        }
    }
    void CheckLook()
    {
        Debug.DrawRay(pointEye1.position, modelTeacher.forward * lookDistance, Color.red);
        if (Physics.Raycast(pointEye1.transform.position, modelTeacher.forward, out RaycastHit hit, lookDistance))
        {
            if (hit.transform != null)
            {
                if (hit.transform.CompareTag("Player") || hit.transform.CompareTag("Classmate"))
                {
                    if (hit.transform.GetComponent<CapsuleCollider>().isTrigger == false)
                    {
                        if (unit.GetComponent<Teacher>() != null)
                        {
                            unit.GetComponent<Teacher>().SomeoneInSight(hit.transform.gameObject);
                        }
                    }
                }
            }
        }

        Debug.DrawRay(pointEye2.position, modelTeacher.forward * lookDistance, Color.red);
        if (Physics.Raycast(pointEye2.transform.position, modelTeacher.forward, out RaycastHit hit2, lookDistance))
        {
            if (hit2.transform != null)
            {
                if (hit2.transform.CompareTag("Player") || hit2.transform.CompareTag("Classmate"))
                {
                    if (hit2.transform.GetComponent<CapsuleCollider>().isTrigger == false)
                    {
                        if (unit.GetComponent<Teacher>() != null)
                        {
                            unit.GetComponent<Teacher>().SomeoneInSight(hit2.transform.gameObject);
                        }
                    }
                }
            }
        }
    }
}
