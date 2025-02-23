using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private int neededCountOfLose;
    [SerializeField] private RectTransform barTransform;
    [Space(20)]
    [SerializeField] private Image[] unitsIcons;
    [SerializeField] private GameObject[] units;

    private float countOfAllMovePixels;
    private float sizeStep;

    private void Start()
    {
        countOfAllMovePixels = barTransform.sizeDelta.x;
        sizeStep = countOfAllMovePixels / neededCountOfLose;
    }

    public void DoIconStep(GameObject go)
    {
        int i = 0;
        foreach (var unit in units)
        {
            if (unit == go)
            {
                if (unitsIcons[i].rectTransform.anchoredPosition.x != countOfAllMovePixels || unitsIcons[i].rectTransform.anchoredPosition.x < countOfAllMovePixels - 1)
                {
                    unitsIcons[i].rectTransform.localPosition += new Vector3(sizeStep, 0, 0);
                }
                if (unitsIcons[i].rectTransform.anchoredPosition.x < countOfAllMovePixels + 1 && unitsIcons[i].rectTransform.anchoredPosition.x > countOfAllMovePixels - 1)
                {
                    Debug.Log(unit.transform.name + " GAME OVER!");
                }
            }
            i++;
        }
    }
}
