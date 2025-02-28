using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TeacherWindowStay : MonoBehaviour
{
    public Teacher teacher;
    public float stayTime;
    public Transform pointForTeacher;

    Vector3 pos;
    Vector3 rot;
    bool isStay;
    float time;
    float time2;

    private void Update()
    {
        if (isStay == true && teacher.currentState.name == teacher.DoNothingStateName && time > 0)
        {
            teacher.currentState.animState = State.AnimState.Stay;
            teacher.transform.position = Vector3.MoveTowards(teacher.transform.position, pointForTeacher.position, 3 * Time.deltaTime);
            teacher.transform.rotation = pointForTeacher.rotation;
            time -= Time.deltaTime;
        }
        else if (isStay == true && teacher.currentState.name != teacher.DoNothingStateName)
        {
            teacher.GetComponent<NavMeshAgent>().enabled = true;
            isStay = false;
        }
        else if (time <= 0)
        {
            isStay = false;
        }

        time2 -= Time.deltaTime;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<Teacher>() != null && isStay == false && time2 <= 0)
        {
            StartSit();
        }
    }

    void StartSit()
    {
        if (teacher.currentState.name == teacher.patrolStateName || teacher.currentState.name == teacher.stayStateName)
        {
            time2 = stayTime + 20F;
            time = stayTime;
            pos = teacher.transform.position;
            rot = teacher.transform.eulerAngles;
            teacher.transform.GetComponent<NavMeshAgent>().enabled = false;
            teacher.GoDoNothingState();
            isStay = true;
        }
    }
}
