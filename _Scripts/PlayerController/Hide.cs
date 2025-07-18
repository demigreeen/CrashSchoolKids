using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hide : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float speed;

    [Space(10)]
    [Header("Hide Place Icon")]
    [SerializeField] private Transform mainCamera;
    [SerializeField] private GameObject inputIcon;
    [SerializeField] private float canvasAutoScalerKoeff = 3;
    [Space(10)]
    [SerializeField] private GameObject mobileIcon;


    private ObjectToHideIn hideInObject;
    private bool isHide;

    private Transform startPlayerTransform;
    private Vector3 startPlayerPosition;
    private Transform startCameraTransform;
    private PlayerController playerController;
    private CapsuleCollider capsuleCollider;
    private Rigidbody rb;

    public event Action hide;

    private bool isMobileTap;
    private void Start()
    {
        playerController = player.gameObject.GetComponent<PlayerController>();
        capsuleCollider = player.gameObject.GetComponent<CapsuleCollider>();
        rb = player.gameObject.GetComponent<Rigidbody>();
    }
    private void Update()
    {
        CheckInput();
        SetCanvasForAvailablePlace();
    }
    void GetIn()
    {
        player.position = Vector3.Lerp(player.position, hideInObject.pointForCamera.position, speed * Time.deltaTime);
        player.rotation = Quaternion.Lerp(player.rotation, hideInObject.pointForCamera.rotation, speed * Time.deltaTime);
        mainCamera.rotation = new Quaternion(0, mainCamera.rotation.y, 0, mainCamera.rotation.w);
        playerController.enabled = false;
        capsuleCollider.isTrigger = true;
        rb.isKinematic = true;
    }

    void GetOut()
    {
        player.position = startPlayerPosition;
        player.rotation = startPlayerTransform.rotation;
        mainCamera.rotation = startCameraTransform.rotation;
        playerController.enabled = true;
        capsuleCollider.isTrigger = false;
        rb.isKinematic = false;
        hideInObject = null;
    }

    public void CheckInput()
    {
       if (hideInObject != null)
       {
            if (Input.GetKeyDown(KeyCode.Q) && isHide == false || isMobileTap == true && isHide == false)
            {
                hide();
                startPlayerTransform = null;
                startCameraTransform = null;
                isHide = true;
                startCameraTransform = mainCamera.transform;
                startPlayerTransform = player.transform;
                startPlayerPosition = player.position;

                isMobileTap = false;
                mobileIcon.SetActive(true);
                GetIn();
            }
            else if (Input.GetKeyDown(KeyCode.Q) && isHide == true || isMobileTap == true && isHide == true)
            {
                if(TutorialManager.instance.isTutorialOn == false )
                {
                    isHide = false;

                    isMobileTap = false;
                    mobileIcon.SetActive(false);
                    GetOut();
                }
                else if (TutorialManager.instance.isTutorialOn == true && TutorialManager.instance.isNotDropped == true)
                {
                    isHide = false;

                    isMobileTap = false;
                    mobileIcon.SetActive(false);
                    GetOut();
                }

            }
       }

       if (isHide == true)
       {
           GetIn();
       }

    }

    void SetCanvasForAvailablePlace()
    {
        if (hideInObject != null && isHide == false)
        {
            inputIcon.SetActive(true);
            inputIcon.transform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, hideInObject.posForIcon.transform.TransformPoint(Vector3.zero));
        }
        else
        {
            inputIcon.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("HidePlace"))
        {
            hideInObject = other.GetComponent<ObjectToHideIn>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("HidePlace") && isHide == false)
        {
            hideInObject = null;
        }
    }

    public void MobileTap()
    {
        isMobileTap = true;
    }
}
