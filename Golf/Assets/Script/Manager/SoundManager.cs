﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{



    public static SoundManager _instance = null;
    public static SoundManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SoundManager>() as SoundManager;
            }
            return _instance;
        }

    }

    public AudioClip[] mFxSound = new AudioClip[10];
    private AudioSource mFxAudio;

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(this);
        mFxAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayFXSound(CommonData.SOUND_TYPE type)
    {
        mFxAudio.clip = mFxSound[(int)type];
        if(mFxAudio.isPlaying == false)
        {
            mFxAudio.Play();
        }        
    }

    public bool IsFxAudioPlay()
    {
        return mFxAudio.isPlaying;
    }

}
