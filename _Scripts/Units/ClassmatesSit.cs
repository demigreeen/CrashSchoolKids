using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClassmatesSit : MonoBehaviour
{
    private Classmate classmate;
    public float sitTime;
    public Transform pointForTeacher;

    Vector3 pos;
    Vector3 rot;
    bool isSit;
    float time;
    float time2;

    private void Update()
    {
        if (classmate != null && isSit == true && classmate.currentState.name == classmate.DoNothingStateName && time > 0)
        {
            classmate.currentState.animState = State.AnimState.Sit;
            classmate.transform.position = Vector3.MoveTowards(classmate.transform.position, pointForTeacher.position, 3 * Time.deltaTime);
            classmate.transform.rotation = pointForTeacher.rotation;
            time -= Time.deltaTime;
        }
        else if (classmate != null && isSit == true && classmate.currentState.name != classmate.DoNothingStateName)
        {
            classmate.GetComponent<NavMeshAgent>().enabled = true;
            isSit = false;
        }
        else if (classmate != null && time <= 0)
        {
            isSit = false;
        }

        time2 -= Time.deltaTime;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<Classmate>() != null && isSit == false && time2 <= 0)
        {
            classmate = other.transform.GetComponent<Classmate>();
            StartSit();
        }
    }

    void StartSit()
    {
        if (classmate.currentState.name == classmate.patrolStateName || classmate.currentState.name == classmate.shockedStateName)
        {
            time2 = sitTime + 30F;
            time = sitTime;
            pos = classmate.transform.position;
            rot = classmate.transform.eulerAngles;
            classmate.transform.GetComponent<NavMeshAgent>().enabled = false;
            classmate.GoDoNothingState();
            isSit = true;
        }
    }
}
