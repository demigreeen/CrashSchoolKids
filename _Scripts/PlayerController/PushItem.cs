using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BzKovSoft.ObjectSlicer.Samples;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;
using static UnityEngine.UI.CanvasScaler;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class PushItem : MonoBehaviour
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
    [Header("Push")]
    [SerializeField] private float pushForce;
    [SerializeField] private float multipluPushUp;
    [SerializeField] private float timeBeforeNextDrag;

    [Space(10)]
    [Header("Detect Sphera Classmates")]
    [SerializeField] private float radius;
    [SerializeField] private LayerMask detectedLayerMask;
    [SerializeField] private LayerMask detectedLayerMask2;

    [HideInInspector] public GameObject holdItem { get; private set; }

    public List<GameObject> nearItems;
    private GameObject nearestItem;
    private Rigidbody pushItemRb;
    private IBzSliceableNoRepeat pushItemSlicer;
    private float timeBeforeNextDragCode;
    public event Action ItemDroped;



    private void Start()
    {
        nearItems = new List<GameObject>();

        StartCoroutine(ICheckDistance());
    }
    private void Update()
    {

        SetCanvasForAvailableItem();
        CheckInput();

        timeBeforeNextDragCode -= Time.deltaTime;
    }


    public void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (nearestItem != null && timeBeforeNextDragCode <= 0)
            {
                Push();
            }
        }
    }

    // Толкане
    void Push()
    {
        if (ItemDroped != null)
        {
            if (FindObjectOfType<Teacher>().currentState.name != FindObjectOfType<Teacher>().angryStateName)
            {
                holdItem = nearestItem;
                ItemDroped();
            }
        }

        pushItemRb = nearestItem.GetComponent<Rigidbody>();
        pushItemRb.isKinematic = false;
        ItemForPush itemForPush = nearestItem.GetComponent<ItemForPush>();

        if (nearestItem.transform.CompareTag("PushItem"))
        {
            pushItemRb.AddForce(new Vector3(UnityEngine.Random.Range(itemForPush.x - 0.03f, itemForPush.x + 0.03f) / 2, UnityEngine.Random.Range(itemForPush.y - 0.03f, itemForPush.y + 0.03f) / 2, UnityEngine.Random.Range(itemForPush.z - 0.03f, itemForPush.z + 0.03f))* pushForce, ForceMode.Impulse);
            pushItemRb.AddForce(new Vector3(0, multipluPushUp, 0) * pushForce, ForceMode.Impulse);
            ForceSimilliarItems();
        }

        timeBeforeNextDragCode = timeBeforeNextDrag;
        pointForIcon = null;
        CheckNearClassmates();
    }

    // Лепим канвас с иконкой на предмет, который ближе всех
    void SetCanvasForAvailableItem()
    {
        if (nearestItem != null && timeBeforeNextDragCode <= 0)
        {
            canvasButton.SetActive(true);
            canvasButton.transform.position = pointForIcon.transform.position;

            canvasButton.transform.LookAt(mainCamera);
            canvasButton.transform.rotation = Quaternion.LookRotation(mainCamera.forward, Vector3.up);

            canvasButton.transform.localScale = new Vector3((Vector3.Distance(mainCamera.position, nearestItem.transform.position) / canvasAutoScalerKoeff), (Vector3.Distance(mainCamera.position, nearestItem.transform.position) / canvasAutoScalerKoeff), (Vector3.Distance(mainCamera.position, nearestItem.transform.position) / canvasAutoScalerKoeff));
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
            pointForIcon = null;
        }
    }

    void ForceSimilliarItems()
    {
        List<GameObject> delItems = new List<GameObject>();
        foreach (var item in nearItems)
        {
            if (nearestItem.transform.name == item.transform.name )
            {
                delItems.Add(item);
                Rigidbody rb = item.GetComponent<Rigidbody>();
                ItemForPush itemForPush = rb.GetComponent<ItemForPush>();
                rb.isKinematic = false;


                rb.AddForce(new Vector3(UnityEngine.Random.Range(itemForPush.x - 0.03f, itemForPush.x + 0.03f), UnityEngine.Random.Range(itemForPush.y - 0.03f, itemForPush.y + 0.03f), UnityEngine.Random.Range(itemForPush.z - 0.03f, itemForPush.z + 0.03f))  * pushForce, ForceMode.Impulse);
                rb.AddForce(new Vector3(0, multipluPushUp, 0) * pushForce, ForceMode.Impulse);
                
            }
        }
        nearItems.Remove(nearestItem.gameObject);
        if (delItems.Count > 0)
        {
            foreach (var item in delItems)
            {
                nearItems.Remove(item.gameObject);
            }
        }
    }

    // Проверяем на наличие одноклассников во время броска
    void CheckNearClassmates()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, detectedLayerMask);
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
        Vector3 directionToPlayer = (classmate.transform.position - transform.position).normalized;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, directionToPlayer, out hit, radius, detectedLayerMask2))
        {
            if (hit.transform != null)
            {
                if (hit.transform.CompareTag("Classmate"))
                {
                    classmate.GetComponent<Classmate>().Shocked();
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
        if (other.transform.CompareTag("PushItem"))
        {
            if (other.transform.GetComponent<Rigidbody>().isKinematic == true)
            {
                nearItems.Add(other.gameObject);
                CheckDistanceToItems();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("PushItem"))
        {
            if (other.transform.GetComponent<Rigidbody>().isKinematic == true)
            {
                nearItems.Remove(other.gameObject);
                CheckDistanceToItems();
            }
        }
    }
}
