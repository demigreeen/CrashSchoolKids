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
    [SerializeField] private Transform pointForUnit;
    [SerializeField] private Transform pointForTeacher;
    [Space(20)]
    [SerializeField] private PlayerController player;
    [SerializeField] private DragAndDropItem playerComponent1;
    [SerializeField] private PushItem playerComponent2;
    [SerializeField] private GameObject playerComponent3;
    [SerializeField] private Hide playerHide;
    [SerializeField] private Rigidbody playerRb;

    [SerializeField] private Teacher teacherComponent;
    [SerializeField] private NavMeshAgent teacher;

    [SerializeField] private GameObject camera;
    [SerializeField] private Transform endPointForCamera;
    [SerializeField] private GameObject[] allUnit;
    [Space(20)]
    [SerializeField] private GameObject canvasDetail1;
    [SerializeField] private GameObject canvasDetail2;

    private Vector3 beginPosUnit;
    private Vector3 beginPosTeacher;
    private GameObject currUnit;
    private Animator teacherAnimator;
    private Animator currUnitAnimator;

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
        teacherAnimator = teacher.transform.GetComponent<Animator>();
        currUnitAnimator = unit.GetComponent<Animator>();

        currUnitAnimator.SetBool("isSad", true);
        teacherAnimator.SetBool("isAngry", true);

        playerHide.enabled = false;
        player.enabled = false;
        playerComponent1.enabled = false;
        playerComponent2.enabled = false;
        playerComponent3.SetActive(true);
        playerRb.isKinematic = true;
        teacherComponent.enabled = false;

        currUnit = unit;
        if (unit.GetComponent<NavMeshAgent>() != null)
        {
            unit.GetComponent<NavMeshAgent>().enabled = false;
        }
        teacher.enabled = false;

        beginPosTeacher = teacher.transform.position;
        beginPosUnit = unit.transform.position;
        teacher.gameObject.transform.position = pointForTeacher.position;
        teacher.gameObject.transform.rotation = pointForTeacher.rotation;
        unit.transform.position = pointForUnit.position;
        unit.transform.rotation = pointForUnit.rotation;

        foreach (GameObject obj in allUnit)
        {
            if (obj != currUnit) { obj.SetActive(false); }
        }

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

        teacherComponent.angryMusic.Stop();

        currUnitAnimator.SetBool("isSad", false);
        teacherAnimator.SetBool("isAngry",false);

        cutscene.SetActive(false);
        player.enabled = true;
        playerHide.enabled = true;
        playerComponent1.enabled = true;
        playerComponent2.enabled = true;
        playerComponent3.SetActive(false);
        playerRb.isKinematic = false;
        canvasDetail1.SetActive(true);
        canvasDetail2.SetActive(true);
        teacherComponent.enabled = true;

        currUnit.transform.position = beginPosUnit;
        teacher.transform.position = beginPosTeacher;
        teacher.GetComponent<Teacher>().basicMusic.Play();

        if (currUnit.GetComponent<NavMeshAgent>() != null)
        {
            currUnit.GetComponent<NavMeshAgent>().enabled = true;
        }
        teacher.enabled = true;

        foreach (GameObject obj in allUnit)
        {
            if (obj != currUnit) { obj.SetActive(true); }
        }

        currUnit = null;
        currUnitAnimator = null;
        if (cutsceneEnd != null) { cutsceneEnd(); }
    }

    
}
