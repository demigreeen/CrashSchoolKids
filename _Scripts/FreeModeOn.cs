using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeModeOn : MonoBehaviour
{
    public GameObject[] gameObjectsForOff;
    public GameObject[] gameObjectsForOffMedium;
    public GameObject[] gameObjectsForOffHard;
    public GameObject[] gameObjectsForOffExtrim;
    public ProgressBar progressBar;
    public Teacher teacher;

    private void Start()
    {
        if (PlayerPrefs.GetInt("mode") == 1)
        {
            progressBar.neededCountOfLose = 1000;
            foreach (GameObject obj in gameObjectsForOff)
            {
                obj.SetActive(false);
            }
        }

        if (PlayerPrefs.GetInt("mode") == 1)
        {
            switch (PlayerPrefs.GetInt("difficult"))
            {
                case 0:
                   
                    break;
                case 1:
                    foreach (GameObject obj in gameObjectsForOffMedium)
                    {
                        obj.SetActive(false);
                    }
                    break;
                case 2:
                    foreach (GameObject obj in gameObjectsForOffHard)
                    {
                        obj.SetActive(false);
                    }
                    break;
                case 3:
                    foreach (GameObject obj in gameObjectsForOffExtrim)
                    {
                        obj.SetActive(false);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
