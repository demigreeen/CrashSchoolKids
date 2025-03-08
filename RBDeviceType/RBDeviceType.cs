using UnityEngine;
using System.Runtime.InteropServices;

public class RBDeviceType : MonoBehaviour {

    public GameObject[] mobileObjForOn;
    public GameObject[] mobileObjForOff;

#if UNITY_EDITOR
    public static bool isMobile()
    {
        return false;
    }
#else
    [DllImport("__Internal")]
    public static extern bool isMobile();
#endif

    void Awake()
    {
        if(isMobile())
        {
            foreach(GameObject obj in mobileObjForOn)
            {
                obj.SetActive(true);
            }
            foreach (GameObject obj in mobileObjForOff)
            {
                obj.SetActive(false);
            }
        }
    }
}
