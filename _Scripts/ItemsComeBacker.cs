using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsComeBacker : MonoBehaviour
{
    public Transform player;
    public Teacher teacher;
    public Transform[] itemsTransfroms;
    public Rigidbody[] itemsRb;

    private Vector3[] itemsStartPos;
    private Vector3[] itemsStartRot;

    private void Start()
    {

        itemsStartPos = new Vector3[itemsTransfroms.Length];
        itemsStartRot = new Vector3[itemsTransfroms.Length];

        for (int i = 0; i < itemsTransfroms.Length; i++)
        {
            itemsStartPos[i] = itemsTransfroms[i].position;
            itemsStartRot[i] = itemsTransfroms[i].eulerAngles;
        }

        StartCoroutine(IComeBack());
    }

    IEnumerator IComeBack()
    {
        yield return new WaitForSeconds(180);
        for (int i = 0; i < itemsTransfroms.Length; i++)
        {
            if (Vector3.Distance(itemsStartPos[i], player.position) > 15 && teacher.currentState.name != teacher.goToDropedItemStateName)
            {
                itemsTransfroms[i].position = itemsStartPos[i];
                itemsTransfroms[i].eulerAngles = itemsStartRot[i];
                itemsRb[i].isKinematic = true;
            }
        }
        StartCoroutine(IComeBack());
    }
}
