using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.AI;

public class TeacherSit : MonoBehaviour
{
    public Teacher teacher;
    public float sitTime;
    public Transform pointForTeacher;

    Vector3 pos;
    Vector3 rot;
    bool isSit;
    float time;
    float time2;

    private void Update()
    {
        if (isSit == true && teacher.currentState.name == teacher.DoNothingStateName && time > 0)
        {
            teacher.currentState.animState = State.AnimState.Sit;
            teacher.transform.position = Vector3.MoveTowards(teacher.transform.position, pointForTeacher.position, 3 * Time.deltaTime);
            teacher.transform.rotation = pointForTeacher.rotation;
            time -= Time.deltaTime;
        }
        else if (isSit == true && teacher.currentState.name != teacher.DoNothingStateName)
        {
            teacher.GetComponent<NavMeshAgent>().enabled = true;
            isSit = false;
        }
        else if(time <= 0)
        {
            isSit = false;
        }

         time2 -= Time.deltaTime;
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<Teacher>() != null && isSit == false && time2 <= 0)
        {
            StartSit();
        }
    }

    void StartSit()
    {
        if (teacher.currentState.name == teacher.patrolStateName || teacher.currentState.name == teacher.stayStateName)
        {
            time2 = sitTime + 30F;
            time = sitTime;
            pos = teacher.transform.position;
            rot = teacher.transform.eulerAngles;
            teacher.transform.GetComponent<NavMeshAgent>().enabled = false;
            teacher.GoDoNothingState();
            isSit = true;
        }
    }
}
