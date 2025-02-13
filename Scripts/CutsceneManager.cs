using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private GameObject cutscene;
    [SerializeField] private Transform pointForUnit;

    private Vector3 beginPosUnit;
    private GameObject currUnit;


    public void StartCutscene(GameObject unit)
    {
        currUnit = unit;
        if (unit.GetComponent<NavMeshAgent>() != null)
        {
            unit.GetComponent<NavMeshAgent>().enabled = false;
        }
        beginPosUnit = unit.transform.position;
        unit.transform.position = pointForUnit.position;

        cutscene.SetActive(true);
    }
    public void EndCutscene()
    {
        cutscene.SetActive(false);

        currUnit.transform.position = beginPosUnit;
        if (currUnit.GetComponent<NavMeshAgent>() != null)
        {
            currUnit.GetComponent<NavMeshAgent>().enabled = true;
        }
        currUnit = null;
    }
}
