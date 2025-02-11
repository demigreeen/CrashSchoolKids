using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float speed;

    [Space(10)]
    [Header("Hide Place Icon")]
    [SerializeField] private Transform mainCamera;
    [SerializeField] private GameObject canvasButton;
    [SerializeField] private float canvasAutoScalerKoeff = 3;


    private ObjectToHideIn hideInObject;
    private bool isHide;

    private Transform startPlayerTransform;
    private PlayerController playerController;
    private CapsuleCollider capsuleCollider;
    private Rigidbody rb;
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
        playerController.enabled= false;
        capsuleCollider.enabled = false;
        rb.isKinematic = true;
    }

    void GetOut()
    {
        player.position = startPlayerTransform.position;
        player.rotation = startPlayerTransform.rotation;
        playerController.enabled = true;
        capsuleCollider.enabled = true;
        rb.isKinematic = false;
        hideInObject = null;
    }

    public void CheckInput()
    {
       if (hideInObject != null)
       {
            if (Input.GetKeyDown(KeyCode.E) && isHide == false)
            {
                startPlayerTransform = null;
                isHide = true;
                startPlayerTransform = player.transform;

                GetIn();
            }
            else if (Input.GetKeyDown(KeyCode.E) && isHide == true)
            {
                isHide = false;

                GetOut();
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
            canvasButton.SetActive(true);
            canvasButton.transform.position = hideInObject.posForIcon.transform.position;

            canvasButton.transform.LookAt(mainCamera);
            canvasButton.transform.rotation = Quaternion.LookRotation(mainCamera.forward, Vector3.up);

            canvasButton.transform.localScale = new Vector3((Vector3.Distance(mainCamera.position, hideInObject.transform.position) / canvasAutoScalerKoeff), (Vector3.Distance(mainCamera.position, hideInObject.transform.position) / canvasAutoScalerKoeff), (Vector3.Distance(mainCamera.position, hideInObject.transform.position) / canvasAutoScalerKoeff));
            canvasButton.SetActive(true);
        }
        else
        {
            canvasButton.SetActive(false);
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
}
