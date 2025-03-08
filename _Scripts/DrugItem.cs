using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrugItem : MonoBehaviour
{
    public bool isCanDrop;

   

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("wall"))
        {
            isCanDrop = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag("wall"))
        {
            isCanDrop = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("wall"))
        {
            isCanDrop = false;
        }
    }
}
