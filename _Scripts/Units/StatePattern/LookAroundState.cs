using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu]
public class LookAroundState : State
{
    public float lookDistance;
    [Space(10)]
    public int repeatCount;

    public float currentRepeatCount;
    private float offsetUpdate;
    private Animator animator;
    private NavMeshAgent agent;

    private GameObject modelTeacher;
    private GameObject pointEye1;
    private GameObject pointEye2;
    public override void Init()
    {
        animator = unit.GetComponent<Animator>();
        agent = unit.GetComponent<NavMeshAgent>();

        if (unit.GetComponent<Teacher>() != null)
        {
            modelTeacher = GameObject.Find("mixamorig:Head");
            pointEye1 = GameObject.Find("PointEye1");
            pointEye2 = GameObject.Find("PointEye2");
        }

    }
    public override void Run()
    {
        CheckLook();
        PlayAnimation();
    }

    void PlayAnimation()
    {
        if (currentRepeatCount < repeatCount && animator.GetCurrentAnimatorStateInfo(0).IsName("LookAround") == false && offsetUpdate <= 0 || currentRepeatCount < repeatCount && currentRepeatCount > 0 && animator.GetCurrentAnimatorStateInfo(0).IsName("LookAround") == true && offsetUpdate <= 0)
        {
            Debug.Log("теуцщее колво повторений:" + currentRepeatCount);
            animState = AnimState.LookAround;
            currentRepeatCount++;
            offsetUpdate = 2.9f;
        }
        else if (offsetUpdate > 0 && currentRepeatCount < repeatCount)
        {
            Debug.Log("теуцщее колво повторений:" + currentRepeatCount);
            offsetUpdate -= Time.deltaTime;
        }
        else if (animState == AnimState.LookAround && currentRepeatCount >= repeatCount && offsetUpdate <= 0)
        {
            Debug.Log(3);
            Debug.Log("теуцщее колво повторений:" + currentRepeatCount);
            animState = AnimState.AngryStay;
            isFinished = true;
        }
        else offsetUpdate -= Time.deltaTime;
    }

    void CheckLook()
    {
        Debug.DrawRay(pointEye1.transform.position, pointEye1.transform.forward * lookDistance, Color.red);
        if (Physics.Raycast(pointEye1.transform.position, pointEye1.transform.forward, out RaycastHit hit, lookDistance))
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

        Debug.DrawRay(pointEye2.transform.position, pointEye2.transform.forward * lookDistance, Color.red);
        if (Physics.Raycast(pointEye2.transform.position, pointEye2.transform.forward, out RaycastHit hit2, lookDistance))
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
