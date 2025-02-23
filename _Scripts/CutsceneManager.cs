using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CutsceneManager : MonoBehaviour
{
    public static CutsceneManager instance;

    [SerializeField] private GameObject cutscene;
    [Space(20)]
    [SerializeField] private NavMeshAgent teacher;
    [SerializeField] private Transform pointForUnit;
    [Space(20)]
    [SerializeField] private PlayerController player;
    [SerializeField] private Hide playerHide;
    [SerializeField] private Rigidbody playerRb;
    [SerializeField] private GameObject camera;
    [SerializeField] private Transform endPointForCamera;
    [Space(20)]
    [SerializeField] private GameObject canvasDetail1;
    [SerializeField] private GameObject canvasDetail2;

    private Vector3 beginPosUnit;
    private Vector3 beginPosTeacher;
    private GameObject currUnit;

    public event Action cutsceneEnd;

    private void Start()
    {

        if (instance == null)
        {
            instance = this;
        }
        else { Destroy(gameObject); }
    }
    private void Update()
    {
        if (cutscene.activeSelf == false)
        {
            MoveCamera();
        }
    }
    public void StartCutscene(GameObject unit)
    {
        playerHide.enabled = false;
        player.enabled = false;
        playerRb.isKinematic = true;

        currUnit = unit;
        if (unit.GetComponent<NavMeshAgent>() != null)
        {
            unit.GetComponent<NavMeshAgent>().enabled = false;
        }
        teacher.enabled = false;

        beginPosTeacher = teacher.transform.position;
        beginPosUnit = unit.transform.position;
        unit.transform.position = pointForUnit.position;

        cutscene.SetActive(true);
    }
    public void EndCutscene()
    {
        StartCoroutine(IWaitInput());
    }

    void MoveCamera()
    {
        camera.transform.position = endPointForCamera.position;
        camera.transform.rotation = endPointForCamera.rotation;
    }

 
    IEnumerator IWaitInput()
    {
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        cutscene.SetActive(false);
        player.enabled = true;
        playerHide.enabled = true;
        playerRb.isKinematic = false;
        canvasDetail1.SetActive(true);
        canvasDetail2.SetActive(true);

        currUnit.transform.position = beginPosUnit;
        teacher.transform.position = beginPosTeacher;

        if (currUnit.GetComponent<NavMeshAgent>() != null)
        {
            currUnit.GetComponent<NavMeshAgent>().enabled = true;
        }
        teacher.enabled = true;

        currUnit = null;
        if (cutsceneEnd != null) { cutsceneEnd(); }
    }

    
}
