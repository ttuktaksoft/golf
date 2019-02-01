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

    public AudioClip[] mFxSound = new AudioClip[4];
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

    public void PlayTempoTraining(float tempo)
    {
        StartCoroutine(Co_TempoTraining(tempo));
    }

    IEnumerator Co_TempoTraining(float tempo)
    {
        while(true)
        {
            SoundManager.Instance.PlayFXSound(CommonData.SOUND_TYPE.TRAINING_SUCCESS);
            yield return new WaitForSeconds(1f);

            SoundManager.Instance.PlayFXSound(CommonData.SOUND_TYPE.TRAINING_START);
            yield return new WaitForSeconds(1f);

            SoundManager.Instance.PlayFXSound(CommonData.SOUND_TYPE.TRAINING_START);
            yield return new WaitForSeconds(1f);

            SoundManager.Instance.PlayFXSound(CommonData.SOUND_TYPE.TRAINING_START);
            yield return new WaitForSeconds(1f);

            SoundManager.Instance.PlayFXSound(CommonData.SOUND_TYPE.TRAINING_START);
            yield return new WaitForSeconds(1f);

            SoundManager.Instance.PlayFXSound(CommonData.SOUND_TYPE.TRAINING_TEMPO);
            yield return new WaitForSeconds(3f);
        }



        yield return null;
    }


    public void PlaySuccessSound()
    {
        StartCoroutine(Co_Success());
    }

    IEnumerator Co_Success()
    {
        SoundManager.Instance.PlayFXSound(CommonData.SOUND_TYPE.TRAINING_SUCCESS);
        yield return new WaitForSeconds(1.5f);

        SoundManager.Instance.PlayFXSound(CommonData.SOUND_TYPE.TRAINING_START);
        yield return new WaitForSeconds(1f);

        SoundManager.Instance.PlayFXSound(CommonData.SOUND_TYPE.TRAINING_START);
        yield return new WaitForSeconds(1f);

        SoundManager.Instance.PlayFXSound(CommonData.SOUND_TYPE.TRAINING_START);
        yield return new WaitForSeconds(1f);

        SoundManager.Instance.PlayFXSound(CommonData.SOUND_TYPE.TRAINING_START);
        yield return new WaitForSeconds(1f);

        SoundManager.Instance.PlayFXSound(CommonData.SOUND_TYPE.TRAINING_SUCCESS);
        yield return new WaitForSeconds(2f);

        TKManager.Instance.bTrainingSuccess = false;

        yield return null;

    }
}
