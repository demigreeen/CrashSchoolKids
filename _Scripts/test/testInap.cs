using TMPro;
using YG;
using YG.Utils.Pay;
using UnityEngine;

public class testInap : MonoBehaviour
{
    [SerializeField] private TMP_Text currentValue;

    private void Update()
    {
        currentValue.text = "" + YG2.saves.isAdBlock;
        Debug.Log(YG2.saves.isAdBlock);
    }

}
