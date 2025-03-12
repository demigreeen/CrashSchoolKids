using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerPrefs = RedefineYG.PlayerPrefs;
public class ActiveIfNotPassGame : MonoBehaviour
{


    private void OnEnable()
    {
        if (PlayerPrefs.GetInt("isPassGame") == 1)
        { gameObject.SetActive(false); }
    }

}
