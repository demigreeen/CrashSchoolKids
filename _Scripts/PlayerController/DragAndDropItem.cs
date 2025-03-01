using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class DragAndDropItem : MonoBehaviour
{
    [Header("Positions For Calculates")]
    [SerializeField] private Transform playerPos;
    [SerializeField] private Transform mainCamera;

    [Space(10)]
    [Header("Item Drag Icon")]
    [SerializeField] private GameObject canvasButton;
    [SerializeField] private float canvasAutoScalerKoeff = 3;

    private Transform pointForIcon;

    [Space(10)]
    [Header("Drag And Drop")]
    [SerializeField] private Transform handsPos;
    [SerializeField] private float speedMoveToHands;
    [SerializeField] LayerMask playerLayerForIgnor;
    [SerializeField] private float forceDrop;
    [SerializeField] private float timeBeforeNextDrag;
    [SerializeField] private Teacher teacher;


   [Space(10)]
    [Header("Detect Sphera Classmates")]
    [SerializeField] private float radius;
    [SerializeField] private LayerMask detectedLayerMask;
    [SerializeField] private LayerMask detectedLayerMask2;
    [HideInInspector] public GameObject holdItem { get; private set; }


     public List<GameObject> nearItems;
     private GameObject nearestItem;
     private bool isHandsEmpty = true;
     private Rigidbody holdItemRb;
     private bool isItemMovedToHands;
     private float timeBeforeNextDragCode;
    private DrugItem drugItem;

    public event Action ItemDroped;

    private void Start()
    {
        nearItems = new List<GameObject>();

        StartCoroutine(ICheckDistance());
    }
    private void Update()
    {
        SetCanvasForAvailableItem();

        if (isHandsEmpty == false) { Hold(); };
        timeBeforeNextDragCode -= Time.deltaTime;

    }


    public void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isHandsEmpty == true && nearestItem != null && timeBeforeNextDragCode <= 0)
            {
                Drag();
            }
            else if (isHandsEmpty == false && drugItem.isCanDrop == true)
            {
                Drop();
            }
        }
    }

    // Подбор
    void Drag()
    {
        holdItem = nearestItem;
        drugItem = holdItem.GetComponent<DrugItem>();
        holdItemRb = nearestItem.GetComponent<Rigidbody>();
        holdItemRb.isKinematic = false;
        holdItemRb.excludeLayers = playerLayerForIgnor;
        isHandsEmpty = false;
    }

    // Удержание
    void Hold()
    {
        holdItem.transform.eulerAngles = new Vector3(mainCamera.transform.eulerAngles.x - 90f, mainCamera.transform.eulerAngles.y, mainCamera.transform.eulerAngles.z );

        if (holdItem.transform.position != handsPos.position && isItemMovedToHands == false)
        {
            holdItem.transform.position = Vector3.MoveTowards(holdItem.transform.position, handsPos.position, speedMoveToHands * Time.deltaTime);
        }
        else if (isItemMovedToHands == true)
        {
            holdItem.transform.position = handsPos.position;
        }
        else
        {
            isItemMovedToHands = true;
        }
    }



    // Бросание
    void Drop()
    {
        if (ItemDroped != null)
        {
            if (FindObjectOfType<Teacher>().currentState.name != FindObjectOfType<Teacher>().angryStateName)
            {
                ItemDroped();
                Debug.Log("drop");
            }
        }

        holdItemRb.isKinematic = true;
        holdItemRb.isKinematic = false;

        holdItemRb.AddForce(mainCamera.forward * forceDrop);

        holdItemRb.excludeLayers = 0;
        holdItem = null;
        holdItemRb = null;
        isItemMovedToHands = false;
        isHandsEmpty = true;

        if (pointForIcon != null && pointForIcon.GetComponent<Point>().audio != null)
        {
            pointForIcon.GetComponent<Point>().audio.Play();
        }

        timeBeforeNextDragCode = timeBeforeNextDrag;

        CheckNearClassmates();
    }

    // Лепим канвас с иконкой на предмет, который ближе всех
    void SetCanvasForAvailableItem()
    {
        if (nearestItem != null && isHandsEmpty == true && timeBeforeNextDragCode <= 0)
        {
            canvasButton.SetActive(true);
            canvasButton.transform.position = pointForIcon.position;

            canvasButton.transform.LookAt(mainCamera);
            canvasButton.transform.rotation = Quaternion.LookRotation(mainCamera.forward, Vector3.up);

            canvasButton.transform.localScale = new Vector3((Vector3.Distance(mainCamera.position, nearestItem.transform.position) / canvasAutoScalerKoeff),(Vector3.Distance(mainCamera.position, nearestItem.transform.position) / canvasAutoScalerKoeff), (Vector3.Distance(mainCamera.position, nearestItem.transform.position) / canvasAutoScalerKoeff));
            canvasButton.SetActive(true);
        }
        else
        {
            canvasButton.SetActive(false);
        }
    }

    // Находим ближайший предмет
    void CheckDistanceToItems()
    {
        if (nearItems.Count != 0)
        {
            nearItems.Sort((a, b) => Vector3.Distance(playerPos.position, a.transform.position).CompareTo(Vector3.Distance(transform.position, b.transform.position)));

            nearestItem = nearItems[0];
            pointForIcon = nearestItem.GetComponentInChildren<Point>().gameObject.transform;
        }
        else
        {
            nearestItem = null;
        }
    }

    // Проверяем на наличие одноклассников во время броска
    void CheckNearClassmates()
    {
        Collider[] hitColliders = Physics.OverlapSphere(playerPos.transform.position, radius, detectedLayerMask);
        if (hitColliders.Length > 0)
        {
            foreach (var unit in hitColliders)
            {
                CheckNearClassmatesLook(unit.gameObject);
            }
        }
    }

    void CheckNearClassmatesLook(GameObject classmate)
    {
        Vector3 directionToPlayer = (classmate.transform.position - playerPos.transform.position);
        directionToPlayer = new Vector3(directionToPlayer.x, directionToPlayer.y + 2, directionToPlayer.z);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, directionToPlayer, out hit, radius, detectedLayerMask2))
        {
           
            if (hit.transform != null)
             {
                Debug.Log(6);
                if (hit.transform.CompareTag("Classmate"))
                 {
                    Debug.Log(7);
                    classmate.GetComponent<Classmate>().Shocked();
                 }
                else
                {
                    Debug.Log(hit.transform.name);
                }
             }
        }
        
    }


    // Если вызывать в update CheckDistanceToItems() могут быть визуальные баги поэтому корутина
    IEnumerator ICheckDistance()
    {
        CheckDistanceToItems();
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(ICheckDistance());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Item"))
        {
            nearItems.Add(other.gameObject);
            CheckDistanceToItems();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Item"))
        {
            nearItems.Remove(other.gameObject);
            CheckDistanceToItems();
        }
    }
}

