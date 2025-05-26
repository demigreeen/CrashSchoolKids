using TMPro;
using UnityEngine;

public class DebugController : MonoBehaviour
{
    [Header("Text Field")]
    [SerializeField] private TMP_Text soundStatus;
    [SerializeField] private TMP_Text volumeStatus;
    [SerializeField] private TMP_Text soundObjectStatus;
    [SerializeField] private TMP_Text musicObjectStatus;

    [Header("GameObjects")]
    [SerializeField] private GameObject soundObjetc;
    [SerializeField] private GameObject musicObjetc;



    private void Update()
    {
        soundStatus.text = $"{AudioListener.pause}";
        volumeStatus.text = $"{AudioListener.volume}";
        soundObjectStatus.text = $"{soundObjetc.activeInHierarchy}";
        musicObjectStatus.text = $"{musicObjetc.activeInHierarchy}";
    }
}
