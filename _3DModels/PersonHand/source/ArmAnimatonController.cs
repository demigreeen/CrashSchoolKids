using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmAnimatonController : MonoBehaviour
{
    [SerializeField] private Animator animatorMain;
    [SerializeField] private DragAndDropItem dropItem;
    void Start()
    {
        PushItem.kickStaticEvent += KickAnimaton;
        Hide.hideStaticEvent += HideAnimaton;
        dropItem.dragItemEvent += TakeAnimaton;
        dropItem.dropItemEvent += Take1Animaton;
    }

    public void MainController(string name)
    {
        if (name == "kick")
        {
            animatorMain.SetBool("isKick", false);
        }
        else if (name == "hide")
        {
            animatorMain.SetBool("isHide", false);
        }
        else if (name == "take")
        {
            animatorMain.SetBool("isTake", false);
            animatorMain.SetBool("isTake1", false);
        }
    }

    private void KickAnimaton()
    {
        if(animatorMain != null)
            animatorMain.SetBool("isKick", true);
    }
    private void HideAnimaton()
    {
        if (animatorMain != null)
            animatorMain.SetBool("isHide", true);
    }
    private void TakeAnimaton()
    {
        if (animatorMain != null)
            animatorMain.SetBool("isTake", true);
    }
    private void Take1Animaton()
    {
        if (animatorMain != null)
            animatorMain.SetBool("isTake1", true);
    }
}
