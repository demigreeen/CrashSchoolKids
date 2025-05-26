using System.Collections;
using System.Collections.Generic;
using Ricimi;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;
using PlayerPrefs = RedefineYG.PlayerPrefs;

public class ButtonManager : MonoBehaviour
{
    private CutsceneManager cutsceneManager;
    private SelectMode selectMode;
    private int currMode;

    private void Start()
    {
        if (YG2.saves.isAdBlock == true)
        {
            YG2.StickyAdActivity(false);
        }

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            cutsceneManager = GetComponent<CutsceneManager>();
        }
        else
        {
            selectMode = GetComponent<SelectMode>();
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Cursor.lockState = CursorLockMode.None;
            LoadScene(0);
        }
    }

    public void SkipCutscene()
    {
        cutsceneManager.EndStartCutscene();
    }

    public void LoadScene(int num)
    {
        if(!YG2.saves.isAdBlock)
            YG2.InterstitialAdvShow();

        SceneManager.LoadScene(num);
        Time.timeScale = 1;
    }

    public void ChangeMode()
    {
        if (currMode == 0)
            currMode++;
        else currMode--;

        selectMode.UpdateCurrentMode(currMode);
    }

    public void OpenSettingsLevelDifficult(GameObject panel)
    {
        if (PlayerPrefs.GetInt("mode") == 1)
        {
            panel.SetActive(true);
            panel.GetComponentInChildren<Popup>().Open();
        }
    }

    public void ChangeLevelDifficult(int level)
    {
        
        PlayerPrefs.SetInt("difficult", level);
        Debug.Log(level);
    }

    public void OpenLink(string link)
    {
        Application.OpenURL(link);
    }
}
