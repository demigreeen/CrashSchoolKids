using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;
using PlayerPrefs = RedefineYG.PlayerPrefs;
public class Energy : MonoBehaviour
{
    public int currentCountOfEnergy;
    public float activeSeconds;
    public Image fillIcon;
    public MovePlayer player;
    public TextMeshProUGUI countText;
    public GameObject[] stopTimerObjects;

    private string rewardID = "1";
    private float timerCode;
    private void Start()
    {
        timerCode = activeSeconds;
    }

    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && fillIcon.transform.gameObject.activeSelf == true)
        {
            GetEnergy();
        }

        if (Input.GetKeyDown(KeyCode.R) && fillIcon.transform.gameObject.activeSelf == true)
        {
            MyRewardAdvShow();
        }

        if (CheckActiveObjectsForStop() == false)
        {
            timerCode += Time.deltaTime;
        }
        fillIcon.fillAmount = timerCode / activeSeconds;
        if (timerCode <= activeSeconds && fillIcon.gameObject.activeSelf == true)
        {
            player.isEnergy = true;
        }
        else
        {
            player.isEnergy = false;
        }
    }

    public void GetEnergy()
    {
        currentCountOfEnergy = PlayerPrefs.GetInt("energy");

        if (currentCountOfEnergy > 0 && fillIcon.fillAmount >= 1)
        {
            currentCountOfEnergy--;

            countText.text = "x" + currentCountOfEnergy.ToString();
            timerCode = 0;
            PlayerPrefs.SetInt("energy", currentCountOfEnergy);
        }
    }

    private void OnEnable()
    {
        currentCountOfEnergy = PlayerPrefs.GetInt("energy");
        countText.text = "x" + currentCountOfEnergy.ToString();
    }

    // Вызов рекламы за вознаграждение
    public void MyRewardAdvShow()
    {
            YG2.RewardedAdvShow(rewardID, () =>
            {
                PlayerPrefs.SetInt("energy", PlayerPrefs.GetInt("energy") + 1);
                countText.text = "x" + PlayerPrefs.GetInt("energy").ToString();
                currentCountOfEnergy = PlayerPrefs.GetInt("energy");
            });
    }

    bool CheckActiveObjectsForStop()
    {
        foreach (var item in stopTimerObjects)
        {
            if (item.activeSelf == true)
            {
                return true;
            }
        }
        return false;
    }
}
