using System.Collections;
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

    public AudioClip[] mFxSound = new AudioClip[20];
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

    public bool PlayFXSound(CommonData.SOUND_TYPE type, bool force = false)
    {

        if(mFxAudio.clip == null)
            mFxAudio.clip = mFxSound[(int)type];

        if (force || mFxAudio.isPlaying == false)
        {
            mFxAudio.clip = mFxSound[(int)type];
            mFxAudio.Play();
        }
        else
        {
            int a = 0;
        }

        return true;
    }

    public bool IsFxAudioPlay()
    {
        return mFxAudio.isPlaying;
    }

}
