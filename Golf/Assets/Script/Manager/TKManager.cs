﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class TKManager : MonoBehaviour
{

    public static TKManager _instance = null;
    public static TKManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<TKManager>() as TKManager;
            }
            return _instance;
        }
    }

    public UserData Mydata = null;
    public CommonData.TRAINING_TYPE TrainingType;
    public CommonData.TRAINING_POSE PoseType;
    public Dictionary<CommonData.TRAINING_ANGLE, int> AngleTypeList = new Dictionary<CommonData.TRAINING_ANGLE, int>();
    public Gyroscope gyro;
    public int TempoTrainingLevel = 3;
    public Sprite ThumbnailSprite = null;
    public bool MirrorMode = true;

    private SaveData MySaveData = new SaveData();
    public bool MyLoadData = false;
    private int TrainingTimer;

    public DateTime CurrSeasonTime = new DateTime(0);
    public DateTime NextSeasonTime = new DateTime(0);
    // Start is called before the first frame update
    void Start()
    {
        // TODO 파베에서 데이터 받아와야함
        Mydata = new UserData();

        FirebaseManager.Instance.init();

        TrainingType = CommonData.TRAINING_TYPE.TRAINING_POSE;
        PoseType = CommonData.TRAINING_POSE.TRAINING_ADDRESS;
        gyro = Input.gyro;
        gyro.enabled = false;
        //Input.location.Start(0.5f);

        DontDestroyOnLoad(this);

        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Application.targetFrameRate = 30;
//#if UNITY_ANDROID || UNITY_EDITOR
//        Screen.SetResolution((int)Screen.safeArea.width, ((int)Screen.safeArea.width * 16) / 9, false);
//#endif
        //Screen.SetResolution((int)Screen.safeArea.width, ((int)Screen.safeArea.width * 16) / 9, false);
    }

    // Update is called once per frame
    public void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown("space"))
        {
            UnityEngine.ScreenCapture.CaptureScreenshot("shot.png");
        }
#endif
    }

    public void SetTrainingType(CommonData.TRAINING_TYPE type)
    {
        this.TrainingType = type;
    }

    public CommonData.TRAINING_TYPE GetTrainingType()
    {
        return this.TrainingType;
    }

    public void SetMode(CommonData.TRAINING_POSE type)
    {
        SetTrainingType(CommonData.TRAINING_TYPE.TRAINING_POSE);
        this.PoseType = type;
    }
    public CommonData.TRAINING_POSE GetPoseType()
    {
        return this.PoseType;
    }

    public void SetAngleType(Dictionary<CommonData.TRAINING_ANGLE, int> dic)
    {
        AngleTypeList.Clear();
        AngleTypeList = dic;
    }

    public Dictionary<CommonData.TRAINING_ANGLE, int> GetAngleType()
    {
        return AngleTypeList;
    }

    public bool IsAngleType(CommonData.TRAINING_ANGLE type)
    {
        return AngleTypeList.ContainsKey(type);
    }

    public int GetAngleLevel(CommonData.TRAINING_ANGLE type)
    {
        if (IsAngleType(type) == false)
            return 0;

        return AngleTypeList[type];
    }

    public void SetTrainingTimer(int Timer)
    {
        TrainingTimer = Timer;
    }

    public int GetTrainingTimer()
    {
        return TrainingTimer;
    }

    public void SetUserLocation()
    {
        //if (Input.location.isEnabledByUser)
        //{
        //    if (Input.location.status != LocationServiceStatus.Failed)
        //    {
        //        Mydata.currentGPSPosition = Input.location.lastData;
        //        FirebaseManager.Instance.SetUserLocation(Mydata.currentGPSPosition);
        //    }
        //    else
        //    {
        //        Debug.Log("!!!!!!Input.location.status" + LocationServiceStatus.Failed);
        //    }
        //}
        //else
        //{
        //    Debug.Log("!!!!!!SetUserLocation not enable" + Input.location.isEnabledByUser);
        //}
    }


    public void ShowLoading()
    {
        PopupMgr.Instance.ShowPopup(PopupMgr.POPUP_TYPE.LOADING);
        //HUD.gameObject.SetActive(true);
        //HUD.ShowHUD(msg, subMsg, waitTime, waitTimeMsg);
    }

    public void HideLoading()
    {
        PopupMgr.Instance.DismissPopup(PopupMgr.POPUP_TYPE.LOADING);
        //HUD.gameObject.SetActive(false);
        //HUD.HideHUD();
    }





    [System.Serializable]
    public class SaveData
    {
        public string Index = "";

        public void Save()
        {
            Index = TKManager.Instance.Mydata.Index;
        }

        public void Load()
        {
            TKManager.Instance.Mydata.Init(Index);
        }
    }



    public void SaveFile()
    {
        MySaveData.Save();
        BinaryFormatter formatter = new BinaryFormatter();
        string path = pathForDocumentsFile("PlayerData2.ini");
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, MySaveData);
        stream.Close();
    }

    public void LoadFile()
    {
        string path = pathForDocumentsFile("PlayerData2.ini");
        FileInfo fileInfo = new FileInfo(path);
        if (fileInfo.Exists)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            MySaveData = (SaveData)formatter.Deserialize(stream);
            stream.Close();
            MySaveData.Load();
            MyLoadData = true;
        }
        else
        {
            //SaveFile();
        }
    }

    public string pathForDocumentsFile(string filename)
    {
#if UNITY_EDITOR
        string path_pc = Application.dataPath;
        path_pc = path_pc.Substring(0, path_pc.LastIndexOf('/'));
        return Path.Combine(path_pc, filename);
#elif UNITY_ANDROID
        string path = Application.persistentDataPath;
        path = path.Substring(0, path.LastIndexOf('/'));
        return Path.Combine(path, filename);
#elif UNITY_IOS
        return Application.persistentDataPath + "/" + filename;
        //string path = Application.dataPath.Substring(0, Application.dataPath.Length);
        //path = path.Substring(0, path.LastIndexOf('/'));
        //return Path.Combine(Path.Combine(path, "Documents"), filename);
#endif
    }
}
