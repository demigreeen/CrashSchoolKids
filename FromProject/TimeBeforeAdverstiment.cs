using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class TimeBeforeAdverstiment : MonoBehaviour
{
    public float timeOffsetAds;

    public static bool isAd = false;
    public GameObject panel;
    public TextMeshProUGUI textTwo;
    public GameObject one;
    public TextMeshProUGUI textOne;
    public GameObject two;

    private bool isAdBlock = false;

    private void Update()
    {
        isAdBlock = YG2.saves.isAdBlock;
    }
    private void Start()
    {
        StartCoroutine(IShowAds());

    }
    IEnumerator IShowAds()
    {
        yield return new WaitForSeconds(timeOffsetAds);
        if (YG2.nowAdsShow == false && YG2.isTimerAdvCompleted == true && !isAdBlock)
        {
            isAd = true;
            YG2.PauseGame(true);
            panel.SetActive(true);
            one.SetActive(true);
            if (YG2.lang == "ru")
            {
                textOne.text = "Реклама через: 2";
            }
            else
            {
                textOne.text = "Advertising via: 2";
            }
            yield return new WaitForSecondsRealtime(1.0f);
            one.SetActive(false);
            two.SetActive(true);
            if (YG2.lang == "ru")
            {
                textTwo.text = "Реклама через: 1";
            }
            else
            {
                textTwo.text = "Advertising via: 1";
            }
            yield return new WaitForSecondsRealtime(1.0f);
            two.SetActive(false);
            panel.SetActive(false);
            YG2.InterstitialAdvShow();
            YG2.PauseGame(false);
        }
        else
        {
            Debug.Log("Полноэкранная реклама еще не доступна.");
        }
        if (!isAdBlock)
        {
            StartCoroutine(IShowAds());
            Debug.Log("Delaem ad");
        }
        else
        {
            Debug.Log("Ne Delaem ad");
        }
    }

}
