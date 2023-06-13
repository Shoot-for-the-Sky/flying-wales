using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public Sound[] bgm, sfx;
    public AudioSource bgmSrc, sfxSrc;
    // Start is called before the first frame update
    void Start()
    {
        // playBgm("MainMenuTheme");
    }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void playBgm(string name)
    {
        Sound s = Array.Find(bgm, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
        }
        else
        {
            bgmSrc.clip = s.clip;
            bgmSrc.Play();
        }
    }

    public void playSfx(string name)
    {
        Sound s = Array.Find(sfx, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
        }
        else
        {
            sfxSrc.clip = s.clip;
            sfxSrc.Play();
        }
    } 
}

