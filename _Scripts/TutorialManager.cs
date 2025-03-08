using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;

    [SerializeField] public DragAndDropItem playerDrag;
    [SerializeField] public PushItem playerPush;
    [SerializeField] public Hide playerHide;
    [SerializeField] public GameObject camera;
    [Space(10)]
    [SerializeField] public GameObject playerBlockCollider;
    [SerializeField] public GameObject boy;
    [SerializeField] public Classmate classmateBoy;
    [SerializeField] public NavMeshAgent agentBoy;
    [SerializeField] public Transform boyPoint;
    [Space(10)]
    [SerializeField] public GameObject girl;
    [Space(10)]
    [SerializeField] public GameObject textBreakChair;
    [SerializeField] public GameObject textHide;

    [HideInInspector] public bool isTutorialOn;

    [HideInInspector] public bool isNotDropped;
    [HideInInspector] public bool isNotHide;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
       

        playerPush.ItemDroped += () => isNotDropped = false;
        playerDrag.ItemDroped += () => isNotDropped = false;
        playerHide.hide += () => isNotHide = false;
    }

    private void Update()
    {
        if (isNotDropped == true && isTutorialOn == true)
        {
            agentBoy.enabled = false;
            classmateBoy.GoDoNothingState();
            classmateBoy.transform.position = boyPoint.position;
            classmateBoy.transform.rotation = boyPoint.rotation;
            classmateBoy.currentState.animState = State.AnimState.Sit;

            textBreakChair.transform.LookAt(camera.transform);
            textBreakChair.transform.rotation = Quaternion.LookRotation(camera.transform.forward);
        }
        else if (isNotHide == true && isTutorialOn == true)
        {
            textHide.transform.LookAt(camera.transform);
            textHide.transform.rotation = Quaternion.LookRotation(camera.transform.forward);
        }
    }
    public void StartTutorial()
    {
        StartCoroutine(ITutorial());
    }
    IEnumerator ITutorial()
    {
        girl.SetActive(false);
        isTutorialOn = true;
        isNotDropped = true;
        isNotHide = true;
        playerBlockCollider.SetActive(true);
        textBreakChair.SetActive(true);

        yield return new WaitUntil(() => isNotDropped == false);

        textBreakChair.SetActive(false);
        textHide.SetActive(true);

        yield return new WaitUntil(() => isNotHide == false);

        textHide.SetActive(false);

        yield return new WaitForSeconds(14f);
        girl.SetActive(true);
        isTutorialOn = false;
        playerBlockCollider.SetActive(false);
    }
}
