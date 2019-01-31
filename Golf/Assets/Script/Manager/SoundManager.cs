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

    public AudioClip[] mFxSound = new AudioClip[3];
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
        yield return new WaitForSeconds(1f);

        yield return null;

    }
}
