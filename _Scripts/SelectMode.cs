using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;

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
            currModeText.text = "ׁ‏זוע";
            PlayerPrefs.SetInt("mode", 0);
        }
        else
        {
            button.sprite = spriteFreeMode;
            currModeText.text = "ׁגמבמהםûי";
            PlayerPrefs.SetInt("mode", 1);
        }
    }
}
