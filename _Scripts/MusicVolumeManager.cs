using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PlayerPrefs = RedefineYG.PlayerPrefs;

public class MusicVolumeManager : MonoBehaviour
{
    public static MusicVolumeManager Instance;

    public AudioSource[] sounds;
    public AudioSource[] musics;

    private float[] soundsVolume;
    private float[] musicsVolume;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        PlayerPrefs.SetInt("NumOfSession", PlayerPrefs.GetInt("NumOfSession") + 1);
        PlayerPrefs.Save();
        if (SceneManager.GetActiveScene().buildIndex == 0 && PlayerPrefs.GetInt("NumOfSession") == 1)
        {
            PlayerPrefs.SetInt("energy", 1);
            PlayerPrefs.SetInt("sound_on", 1);
            PlayerPrefs.SetInt("music_on", 1);
            
        }
        AudioListener.volume = 1f;

        soundsVolume = new float[sounds.Length];
        musicsVolume = new float[musics.Length];

        for (int i = 0; i < sounds.Length; i++)
        {
            soundsVolume[i] = sounds[i].volume;
        }
        for (int i = 0; i < musics.Length; i++)
        {
            musicsVolume[i] = musics[i].volume;
        }

        if (PlayerPrefs.GetInt("sound_on") == 1)
        {
            for (int i = 0; i < sounds.Length; i++)
            {
                sounds[i].volume = soundsVolume[i];
            }
        }
        else
        {
            for (int i = 0; i < sounds.Length; i++)
            {
                sounds[i].volume = 0;
            }
        }

        if (PlayerPrefs.GetInt("music_on") == 1)
        {
            for (int i = 0; i < musics.Length; i++)
            {
                musics[i].volume = musicsVolume[i];
            }
        }
        else
        {
            for (int i = 0; i < musics.Length; i++)
            {
                musics[i].volume = 0f;
            }
        }
    }



    public void UpdateMusicVolume()
    {
        if (PlayerPrefs.GetInt("music_on") == 1)
        {
            for (int i = 0; i < musics.Length; i++)
            {
                musics[i].volume = musicsVolume[i];
            }
        }
        else
        {
            for (int i = 0; i < musics.Length; i++)
            {
                musics[i].volume = 0f;
            }
        }
    }
    public void UpdateSoundVolume()
    {
        if (PlayerPrefs.GetInt("sound_on") == 1)
        {
            for (int i = 0; i < sounds.Length; i++)
            {
                sounds[i].volume = soundsVolume[i];
            }
        }
        else
        {
            for (int i = 0; i < sounds.Length; i++)
            {
                sounds[i].volume = 0;
            }
        }
    }

}
