using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
