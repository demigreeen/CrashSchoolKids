using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu]
public class MoveToDropItemState : State
{

    public float visionAngle;
    public float visionStartDistance;
    public float visionDistance;
    public float visionRadius;
    public LayerMask detectedLayerMask;
    [Space(10)]
    public float speed;

    private float distToItem = 2.5f;
    private Vector3 targetPos;
    private NavMeshAgent agent;
    private Transform item;
    private Animator animator;

    private Transform modelTeacher;
    private Transform pointEye1;
    private Transform pointEye2;

    private float timerBeforeStartLook;
    private GameObject probablyTargerUnit;


    public override void Init()
    {
        if (unit.GetComponent<Teacher>() != null)
        {
            modelTeacher = GameObject.Find("ModelTeacher").GetComponent<Transform>();
            pointEye1 = GameObject.Find("PointEye1").GetComponent<Transform>();
            pointEye2 = GameObject.Find("PointEye2").GetComponent<Transform>();
        }

        animator = unit.GetComponent<Animator>();
        agent = unit.GetComponent<NavMeshAgent>();
        agent.speed = speed;

        item = FindObjectOfType<DragAndDropItem>().holdItem.transform;

        targetPos = item.position;
        agent.destination = targetPos;

        timerBeforeStartLook = 1;
    }
    public override void Run()
    {
        timerBeforeStartLook -= Time.deltaTime;
        animState = AnimState.Run;
        Move();
    }

    void Move()
    {
        targetPos = item.position;

        if (agent.destination != targetPos && Vector3.Distance(unit.transform.position, targetPos) >= distToItem)
        {
            agent.destination = targetPos;
        }
        else if (Vector3.Distance(unit.transform.position, targetPos) <= distToItem)
        {
            agent.speed = 0;
            isFinished = true;
        }


        if (timerBeforeStartLook < 0)
        {
                    CheckLook();
        }
    }

    void CheckLook()
    {
        CheckUnits();

        if (probablyTargerUnit != null)
        {
            Vector3 directionToPlayer = (probablyTargerUnit.transform.position - unit.transform.position).normalized;
            float angleToPlayer = Vector3.Angle(unit.transform.forward, directionToPlayer);
            if (angleToPlayer < visionAngle / 2)
            {
                RaycastHit hit;
                if (Physics.Raycast(unit.transform.position, directionToPlayer, out hit, visionDistance))
                {
                    if (hit.transform != null)
                    {
                        if (hit.transform.CompareTag("Player") || hit.transform.CompareTag("Classmate"))
                        {
                            unit.GetComponent<Teacher>().SomeoneInSight(hit.transform.gameObject);
                        }
                    }
                }
            }
        }
   }

    void CheckUnits()
    {
        if (Vector3.Distance(unit.transform.position, targetPos) < visionStartDistance)
        {
            Collider[] hitColliders = Physics.OverlapSphere(unit.transform.position, visionRadius, detectedLayerMask);
            if (hitColliders.Length > 0)
            {
                List<Collider> hitCollidersList = new List<Collider>();
                foreach (var unit in hitColliders)
                {
                    hitCollidersList.Add(unit);
                }
                hitCollidersList.Sort((a, b) => Vector3.Distance(unit.transform.position, a.transform.position).CompareTo(Vector3.Distance(unit.transform.position, b.transform.position)));

                probablyTargerUnit = hitCollidersList[0].transform.gameObject;
            }
        }
    }

}
