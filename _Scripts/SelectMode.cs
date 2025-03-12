using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;
using static Cinemachine.DocumentationSortingAttribute;
using PlayerPrefs = RedefineYG.PlayerPrefs;

public class SelectMode : MonoBehaviour
{
    public Image button;
    public Sprite spriteDefaultMode;
    public Sprite spriteFreeMode;
    public TextMeshProUGUI currModeText;

    private void Start()
    {
        PlayerPrefs.SetInt("mode", 0);
        PlayerPrefs.SetInt("difficult", 0);
    }
    public void UpdateCurrentMode(int mode)
    {
        if (mode == 0)
        {
            button.sprite = spriteDefaultMode;
            if (YG2.lang == "ru")
            {
                currModeText.text = "ׁ‏זוע";
            }
            else
            {
                currModeText.text = "Story";
            }
            PlayerPrefs.SetInt("mode", 0);
        }
        else
        {
            button.sprite = spriteFreeMode;
            if (YG2.lang == "ru")
            {
                currModeText.text = "ׁגמבמהםûי";
            }
            else
            {
                currModeText.text = "Free";
            }
            PlayerPrefs.SetInt("mode", 1);
        }
    }
}
