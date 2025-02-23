using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu]
public class AngryStayState : State
{
    public float stayTime;

    private NavMeshAgent agent;
    private Animator animator;
    public LayerMask detectedLayerMask;
    [Space(10)]
    public float visionAngle;
    public float visionDistance;
    public float visionRadius;
    private GameObject probablyTargerUnit;


    public override void Init()
    {
        animator = unit.GetComponent<Animator>();
        agent = unit.GetComponent<NavMeshAgent>();

    }


    public override void Run()
    {
        stayTime -= Time.deltaTime;
        if (stayTime <= 0)
        {
            isFinished = true;
        }
        else
        {
            agent.speed = 0;
            animState = AnimState.Stay;
        }

        CheckLook();
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

