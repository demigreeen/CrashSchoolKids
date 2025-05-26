using YG;
using UnityEngine;

public class PayButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject buyButton;
    [SerializeField] private GameObject OffAdPanel;
    [SerializeField] private GameObject isSellPanel;
    void Update()
    {
        if(!YG2.saves.isAdBlock)
        {
            buyButton.SetActive(true);
            OffAdPanel.SetActive(false);
            isSellPanel.SetActive(false);
        }
        else
        {
            buyButton.SetActive(false);
            OffAdPanel.SetActive(true);
            isSellPanel.SetActive(true);
        }
    }
}
