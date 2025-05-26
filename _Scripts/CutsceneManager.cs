using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using YG;
using PlayerPrefs = RedefineYG.PlayerPrefs;

public class CutsceneManager : MonoBehaviour
{
    public static CutsceneManager instance;

    [Header("ANGRY CUTSCENE")]
    public GameObject cutscene;
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
    [SerializeField] private GameObject canvasDetail3;
    [SerializeField] private GameObject canvasDetail4;
    [SerializeField] private GameObject canvasDetail5;
    [SerializeField] private GameObject canvasDetail6;
    [SerializeField] private GameObject pressSpaceText;
    [SerializeField] private GameObject pressSpaceTextPC;
    [SerializeField] private GameObject pressForContinueMobile;

    private Vector3 beginPosUnit;
    private Vector3 beginPosTeacher;
    private GameObject currUnit;
    private Animator teacherAnimator;
    private Animator currUnitAnimator;

    public event Action cutsceneEnd;
    [Header("START CUTSCENE")]
    [SerializeField] private bool isNeedToShowCutscene;
    [SerializeField] private bool isNeedToShowTurorial;
    [Space(30)]
    [SerializeField] private GameObject startCutscene;
    [Space(20)]
    [SerializeField] private Animator animatorBoy;
    [SerializeField] private Animator animatorGirl;
    [SerializeField] private Animator animatorPlayer;
    [SerializeField] private Animator animatorTeacher;
    [SerializeField] private NavMeshAgent agentBoy;
    [SerializeField] private NavMeshAgent agentGirl;
    [SerializeField] private NavMeshAgent agentPlayer;
    [SerializeField] private NavMeshAgent agentTeacher;
    [Space(20)]
    [SerializeField] private GameObject[] allOffUnit;
    [SerializeField] private GameObject playerGo;
    [SerializeField] private Transform playerStartPoint;
    [SerializeField] private GameObject skipCutsceneIcon;
    [SerializeField] private GameObject learningPanel;
    [Space(10)]
    [SerializeField] private Transform teacherStartPos;

    [Header("GAME OVER CUTSCENE")]
    [SerializeField] private GameObject gameOverCutscene;

    [SerializeField] private Animator animatorBoy2;
    [SerializeField] private Animator animatorGirl2;
    [SerializeField] private Animator animatorPlayer2;
    [SerializeField] private Animator animatorTeacher2;
    [SerializeField] private NavMeshAgent agentTeacher2;
    [SerializeField] private GameObject boyGo;
    [SerializeField] private GameObject playerBaseGo;
    [SerializeField] private GameObject girlGo;
    [SerializeField] private Transform firstPointPlayer;
    [SerializeField] private Transform firstPointBoy;
    [SerializeField] private Transform firstPointGirl;
    [SerializeField] private GameObject teacherHand;
    [SerializeField] private Animator door1;
    [SerializeField] private Animator door2;
    [SerializeField] private Transform lastPoint;
    [SerializeField] private Transform victoryPoint;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private AudioSource loseAudio1;
    [SerializeField] private AudioSource loseAudio2;
    [SerializeField] private AudioSource winAudio1;
    [SerializeField] private AudioSource winAudio2;

    [Header("HIT CUTSCENE")]
    [SerializeField] private GameObject hitCutscene;
    [SerializeField] private GameObject playerComponent4;
    [SerializeField] private GameObject playerComponent5;
    [Space(50)]
    [Header("OTHER")]
    [SerializeField] private GameObject personArm;
    [Header("MUSIC")]
    [SerializeField] private AudioSource backMusicCutscene;

    private bool isGirl;
    private bool isBoy;
    private bool isPlayer;
    private bool isMobileTap;
    private bool isCutscene = false;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else { Destroy(gameObject); }

        if (PlayerPrefs.GetInt("mode") == 0)
        {
            isNeedToShowCutscene = true;
            isNeedToShowTurorial = true;
        }
        else
        {
            isNeedToShowCutscene = false;
            isNeedToShowTurorial = false;
        }

        if (isNeedToShowCutscene == true)
        {
            StartStartCutscene();
        }
        else
        {
            playerGo.transform.position = playerStartPoint.position;

            player.enabled = true;
            playerHide.enabled = true;
            playerComponent1.enabled = true;
            playerComponent2.enabled = true;
            playerComponent3.SetActive(false);
            playerRb.isKinematic = false;
            canvasDetail1.SetActive(true);
            canvasDetail2.SetActive(true);
            canvasDetail3.SetActive(true);
            canvasDetail5.SetActive(true);
            canvasDetail6.SetActive(true);
            if (RBDeviceType.MyIsMobile())
                canvasDetail4.SetActive(true);

            if (isNeedToShowTurorial == true)
            {
                TutorialManager.instance.StartTutorial();
            }
            else
            {
                teacher.transform.position = teacherStartPos.position;
            }
        }

    }
    private void Update()
    {
        if (cutscene.activeSelf == false)
        {
            MoveCamera();
        }
        else if(currUnit.GetComponent<NavMeshAgent>() != null && cutscene.activeSelf == true && currUnit != null)
        {
            currUnit.GetComponent<NavMeshAgent>().enabled = false;
        }

    }
    public void StartCutscene(GameObject unit)
    {
        EndHitCutscene();

        isCutscene = true;
        personArm.SetActive(false);
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

        if (PlayerPrefs.GetInt("mode") == 0)
        {
            foreach (GameObject obj in allUnit)
            {
                if (obj != currUnit) { obj.SetActive(false); }
            }
        }

        cutscene.SetActive(true);

        UnityEngine.Cursor.lockState = CursorLockMode.Confined;

        if (RBDeviceType.MyIsMobile() == false)
        {
            pressSpaceTextPC.SetActive(true) ;
        }
        else
        {
            pressForContinueMobile.SetActive(true) ;
        }

    }

    public void EndCutscene()
    {
        StartCoroutine(IWaitInput());
    }
    public void StartStartCutscene()
    {
        backMusicCutscene.Play();
        isCutscene = true;
        startCutscene.SetActive(true);
        foreach (var item in allOffUnit)
        {
            item.SetActive(false);
        }
        agentBoy.enabled = false;
        agentGirl.enabled = false;
        agentPlayer.enabled = false;
        agentTeacher.enabled = false;

        playerHide.enabled = false;
        player.enabled = false;
        playerComponent1.enabled = false;
        playerComponent2.enabled = false;
        playerComponent3.SetActive(true);
        playerRb.isKinematic = true;

        UnityEngine.Cursor.lockState = CursorLockMode.Confined;
        skipCutsceneIcon.SetActive(true);
    }
    public void EndStartCutscene()
    {
        backMusicCutscene.Stop();
        isCutscene = false;
        Debug.Log("is Stoped");
        learningPanel.SetActive(true);
        StopProcess();
        startCutscene.SetActive(false);
        foreach (var item in allOffUnit)
        {
            item.SetActive(true);
        }
        agentBoy.enabled = true;
        agentGirl.enabled = true;
        agentPlayer.enabled = true;
        agentTeacher.enabled = true;

        player.enabled = true;
        playerHide.enabled = true;
        playerComponent1.enabled = true;
        playerComponent2.enabled = true;
        playerComponent3.SetActive(false);
        playerRb.isKinematic = false;
        canvasDetail1.SetActive(true);
        canvasDetail2.SetActive(true);
        canvasDetail3.SetActive(true);
        canvasDetail5.SetActive(true);
        canvasDetail6.SetActive(true);
        if (RBDeviceType.MyIsMobile())
            canvasDetail4.SetActive(true);

        playerGo.transform.position = playerStartPoint.position;

        TutorialManager.instance.StartTutorial();


        skipCutsceneIcon.SetActive(false);
        //if (RBDeviceType.isMobile() == false)
        //{
        //    UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        //}
    }

    private void StopProcess()
    {
        Time.timeScale = 0;
    }
    public void PlayProcess()
    {
        Time.timeScale = 1;
        if (RBDeviceType.MyIsMobile() == false)
        {
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void ChangeBoyAnimation(int numState)
    {
        animatorBoy.SetInteger("State", numState);
    }
    public void ChangeGirlAnimation(int numState)
    {
        animatorGirl.SetInteger("State", numState);
    }
    public void ChangePlayerAnimation(int numState)
    {
        animatorPlayer.SetInteger("State", numState);
    }
    public void ChangeTeacherAnimation(int numState)
    {
        animatorTeacher.SetInteger("State", numState);
    }
    public void TeacherWalk(Transform point)
    {
        agentTeacher.enabled = true;
        agentTeacher.speed = 4f;
        agentTeacher.destination = point.position;
    }
    public void TeacherWalkSlow(Transform point)
    {
        agentTeacher2.enabled = true;
        agentTeacher2.speed = 2f;
        agentTeacher2.destination = point.position;
    }
    public void BoyRun(Transform point)
    {
        agentBoy.enabled = true;
        agentBoy.speed = 5f;
        agentBoy.destination = point.position;
    }
    public void TeacherRotate(Transform point)
    {
        agentTeacher.gameObject.transform.rotation = point.rotation;
        agentTeacher.gameObject.transform.position = point.position;
    }
    public void BoyRotate(Transform point)
    {
        agentBoy.enabled = false;
        agentBoy.gameObject.transform.rotation = point.rotation;
        agentBoy.gameObject.transform.position = point.position;
    }

    public void PushPC(Rigidbody PC)
    {
        Rigidbody pushItemRb = PC.GetComponent<Rigidbody>();
        pushItemRb.isKinematic = false;
        pushItemRb.AddForce(new Vector3(2f, 0.5f, 0) * 2F, ForceMode.Impulse);
        pushItemRb.AddForce(new Vector3(0, 0.5f, 0) * 1.5f, ForceMode.Impulse);
    }
    void MoveCamera()
    {
        camera.transform.position = endPointForCamera.position;
        camera.transform.rotation = endPointForCamera.rotation;
    }

    public void StartGameOvetCutscene(string nameUnit)
    {
        EndHitCutscene();

        gameOverCutscene.SetActive(true);

        playerHide.enabled = false;
        player.enabled = false;
        playerComponent1.enabled = false;
        playerComponent2.enabled = false;
        playerComponent3.SetActive(true);
        playerRb.isKinematic = true;
        teacherComponent.enabled = false;
        foreach (var item in allOffUnit)
        {
            item.SetActive(false);
        }

        if (nameUnit == playerBaseGo.transform.name)
        {
            playerBaseGo.transform.parent = teacherHand.transform;
            playerBaseGo.transform.position = firstPointPlayer.position;
            playerBaseGo.transform.rotation = firstPointPlayer.rotation;
            isPlayer = true;

            boyGo.transform.position = victoryPoint.position;
            boyGo.transform.rotation = victoryPoint.rotation;
        }
        else if (nameUnit == boyGo.transform.name)
        {
            boyGo.transform.parent = teacherHand.transform;
            boyGo.transform.position = firstPointBoy.position;
            boyGo.transform.rotation = firstPointBoy.rotation;
            isBoy = true;

            playerBaseGo.transform.position = victoryPoint.position;
            playerBaseGo.transform.rotation = victoryPoint.rotation;
        }
        else if (nameUnit == girlGo.transform.name)
        {
            girlGo.transform.parent = teacherHand.transform;
            girlGo.transform.position = firstPointGirl.position;
            girlGo.transform.rotation = firstPointGirl.rotation;
            isGirl = true;

            playerBaseGo.transform.position = victoryPoint.position;
            playerBaseGo.transform.rotation = victoryPoint.rotation;
        }
        UnityEngine.Cursor.lockState = CursorLockMode.None;
    }

    public void OpenDoors()
    {
        door1.SetTrigger("open");
        door2.SetTrigger("open");
    }

    public void UnitToLastPoint()
    {
        if (isPlayer == true)
        {
            playerBaseGo.transform.parent = null;
            playerBaseGo.transform.position = lastPoint.position;
            playerBaseGo.transform.rotation = lastPoint.rotation;
        }
        else if (isBoy == true)
        {
            boyGo.transform.parent = null;
            boyGo.transform.position = lastPoint.position;
            boyGo.transform.rotation = lastPoint.rotation;
        }
        else if (isGirl == true)
        {
            girlGo.transform.parent = null;
            girlGo.transform.position = lastPoint.position;
            girlGo.transform.rotation = lastPoint.rotation;
        }
    }
    public void DanceAnimate()
    {
        if (isPlayer == true)
        {
            animatorBoy2.SetInteger("State", 2);
            Invoke("Lose1", 1);
            Invoke("Lose", 3);
        }
        else if (isBoy == true)
        {
            animatorPlayer2.SetInteger("State", 2);
            Invoke("Win1", 2);
            Invoke("Win", 3);
        }
        else if (isGirl == true)
        {
            animatorPlayer2.SetInteger("State", 2);
            Invoke("Win1", 2);
            Invoke("Win", 3);
        }
    }
    void Win()
    {
        PlayerPrefs.SetInt("isPassGame", 1);
        PlayerPrefs.Save();
        winPanel.SetActive(true);
        winAudio2.Play();
        YG2.ReviewShow();
    }
    void Win1()
    {
        winAudio1.Play();
    }
    void Lose()
    { losePanel.SetActive(true);
        loseAudio2.Play();
    }
    void Lose1()
    {
        loseAudio1.Play();
    }
    public void UnitAnimateFly()
    {
        DanceAnimate();

        if (isPlayer == true)
        {
            animatorPlayer2.SetInteger("State", 1);
        }
        else if (isBoy == true)
        {
            animatorBoy2.SetInteger("State", 1);
        }
        else if (isGirl == true)
        {
            animatorGirl2.SetInteger("State", 1);
        }
    }
    public void ChangeTeacherAnimation2(int numState)
    {
        animatorTeacher2.SetInteger("State", numState);
    }
    public void MobileTap ()
    {
        isMobileTap = true;
    }
    IEnumerator IWaitInput()
    {
        if (RBDeviceType.MyIsMobile() == false)
        {
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }
        else
        {
            yield return new WaitUntil(() => isMobileTap == true);
        }

        personArm.SetActive(true);
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
        if (PlayerPrefs.GetInt("mode") == 0)
        {
            canvasDetail2.SetActive(true);
        }
        canvasDetail3.SetActive(true);
        canvasDetail5.SetActive(true);
        canvasDetail6.SetActive(true);
        if (RBDeviceType.MyIsMobile())
            canvasDetail4.SetActive(true);
        teacherComponent.enabled = true;

        currUnit.transform.position = beginPosUnit;
        teacher.transform.position = beginPosTeacher;
        teacher.GetComponent<Teacher>().basicMusic.Play();

        if (currUnit.GetComponent<NavMeshAgent>() != null)
        {
            currUnit.GetComponent<NavMeshAgent>().enabled = true;
        }
        teacher.enabled = true;
        if (PlayerPrefs.GetInt("mode") == 0)
        {
            foreach (GameObject obj in allUnit)
            {
                if (obj != currUnit) { obj.SetActive(true); }
            }
        }


        currUnit = null;
        currUnitAnimator = null;
        if (cutsceneEnd != null) { cutsceneEnd(); }
        if (RBDeviceType.MyIsMobile() == false)
        {
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        }
        pressSpaceText.SetActive(false);
        pressSpaceTextPC.SetActive(false);
        pressForContinueMobile.SetActive(false);

        isMobileTap = false;

        if(!YG2.saves.isAdBlock)
            YG2.InterstitialAdvShow();
    }

    public void StartHitCutscene()
    {
        hitCutscene.SetActive(true);

        playerHide.enabled = false;
        player.enabled = false;
        playerComponent1.enabled = false;
        playerComponent2.enabled = false;
        playerComponent4.SetActive(false);
        playerComponent5.SetActive(false);
        playerRb.isKinematic = true;

        teacher.enabled = false;
        teacherComponent.GetComponent<Animator>().SetTrigger("Hit");

    }

    public void EndHitCutscene()
    {
        hitCutscene.SetActive(false);

        playerHide.enabled = true;
        player.enabled = true;
        playerComponent1.enabled = true;
        playerComponent2.enabled = true;
        playerComponent4.SetActive(true);
        playerComponent5.SetActive(true);
        playerRb.isKinematic = false;

        teacher.enabled = true;
    }
}
