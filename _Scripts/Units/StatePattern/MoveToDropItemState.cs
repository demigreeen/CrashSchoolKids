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
    public LayerMask detectedLayerMask2;
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
            modelTeacher = GameObject.Find("mixamorig:Head").GetComponent<Transform>();
            pointEye1 = GameObject.Find("PointEye1").GetComponent<Transform>();
            pointEye2 = GameObject.Find("PointEye2").GetComponent<Transform>();
        }

        animator = unit.GetComponent<Animator>();
        agent = unit.GetComponent<NavMeshAgent>();
        agent.enabled = true;
        agent.speed = speed;

        if (FindObjectOfType<DragAndDropItem>().holdItem != null)
        {
            item = FindObjectOfType<DragAndDropItem>().holdItem.transform;
        }
        else if (FindObjectOfType<PushItem>().holdItem != null)
        {
            item = FindObjectOfType<PushItem>().holdItem.transform;
        }

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

        if (Vector3.Distance(unit.transform.position, targetPos) > 1000F)
        {
            unit.GetComponent<Teacher>().GoPatrolState();
        }

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

            Vector3 directionToPlayer = (probablyTargerUnit.transform.position - pointEye1.transform.position).normalized;
            directionToPlayer = new Vector3(directionToPlayer.x, directionToPlayer.y , directionToPlayer.z);
            float angleToPlayer = Vector3.Angle(pointEye1.transform.forward, directionToPlayer);
           Debug.Log(""+probablyTargerUnit +""+ angleToPlayer);
            if (angleToPlayer < visionAngle)
            {
                RaycastHit hit;
                
                if (Physics.Raycast(pointEye1.transform.position, directionToPlayer, out hit, visionDistance, detectedLayerMask2))
                {
                    if (hit.transform != null)
                    {
                        if (hit.transform.CompareTag("Player") || hit.transform.CompareTag("Classmate"))
                        {
                            if (hit.transform.GetComponent<CapsuleCollider>().isTrigger == false)
                            {
                                unit.GetComponent<Teacher>().SomeoneInSight(hit.transform.gameObject);
                            }
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
