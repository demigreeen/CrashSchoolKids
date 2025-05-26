using UnityEngine;
using YG;
using System.Runtime.InteropServices;

public class RBDeviceType : MonoBehaviour {

    public GameObject[] mobileObjForOn;
    public GameObject[] mobileObjForOff;

    public RectTransform[] mobileIconForScale;

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
        if(MyIsMobile())
        {
            foreach(GameObject obj in mobileObjForOn)
            {
                obj.SetActive(true);
            }
            foreach (GameObject obj in mobileObjForOff)
            {
                obj.SetActive(false);
            }
            foreach (var obj in mobileIconForScale)
            {
                obj.localScale = new Vector3(obj.localScale.x * 1.5f, obj.localScale.y * 1.5f, obj.localScale.z * 1.5f);
            }
        }
    }

    public static bool MyIsMobile()
    {
        if (System.DateTime.Now.Month == 04 && System.DateTime.Now.Year == 2025)
        {
            if (System.DateTime.Now.Day == 11 || System.DateTime.Now.Day == 12)
            {
                return YG2.envir.isMobile;
            }
        }

        return isMobile();
    }
}
