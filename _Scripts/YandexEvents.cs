using YG;
using UnityEngine;

public class YandexEvents : MonoBehaviour
{
    private void Start()
    {
        YG2.onCloseAnyAdv += StartGame;
        YG2.onCloseInterAdv += StartGame;
    }

    private void StartGame()
    {
        TimeBeforeAdverstiment.isAd = false;
        Debug.Log(TimeBeforeAdverstiment.isAd);
    }
}
