// Copyright (C) 2015-2021 ricimi - All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement.
// A Copy of the Asset Store EULA is available at http://unity3d.com/company/legal/as_terms.

using UnityEngine;
using YG;
using PlayerPrefs = RedefineYG.PlayerPrefs;

namespace Ricimi
{
    // This class is responsible for loading the next scene in a transition (the core of
    // this work is performed in the Transition class, though).
    public class SceneTransition : MonoBehaviour
    {
        public string scene = "<Insert scene name>";
        public float duration = 1.0f;
        public Color color = Color.black;

        public void PerformTransition()
        {
            if (PlayerPrefs.GetInt("mode") == 0 || gameObject.transform.name == "SettingsPlay")
            {
                YG2.InterstitialAdvShow();
                Transition.LoadLevel(scene, duration, color);
            }
        }
    }
}
