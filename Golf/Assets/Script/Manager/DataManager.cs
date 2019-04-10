using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager _instance = null;
    public static DataManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<DataManager>() as DataManager;
            }
            return _instance;
        }
    }

    void Start()
    {
        DontDestroyOnLoad(this);
    }

    public List<AlarmData> AlarmDataList = new List<AlarmData>();
    public Dictionary<TutorialData.TUTORIAL_TYPE, List<TutorialData>> TutorialDataList = new Dictionary<TutorialData.TUTORIAL_TYPE, List<TutorialData>>();
    public string AcademyURL = "";
    public string MarketURL = "";

    public List<SeasonRewardData> SeasonRewardDataList = new List<SeasonRewardData>();

    public void init()
    {
        //AlarmDataList.Add(new AlarmData("새해인사", DateTime.Now.Ticks, "alram_1"));
        //AlarmDataList.Add(new AlarmData("아카데미 소개", DateTime.Now.Ticks, "alram_2"));
        //AlarmDataList.Add(new AlarmData("아카데미 안내", DateTime.Now.Ticks, "alram_3"));
        //AlarmDataList.Add(new AlarmData("어플 사용방법", DateTime.Now.Ticks, "alram_4"));
        //AlarmDataList.Add(new AlarmData("정식버전 출시 일정", DateTime.Now.Ticks, "alram_5"));
    }

    public void AddTutorialData(string type, TutorialData data)
    {
        TutorialData.TUTORIAL_TYPE enumType = TutorialData.TUTORIAL_TYPE.MAIN;

        if (type == "main")
            enumType = TutorialData.TUTORIAL_TYPE.MAIN;
        else if (type == "pose")
            enumType = TutorialData.TUTORIAL_TYPE.POSE;
        else if (type == "angle")
            enumType = TutorialData.TUTORIAL_TYPE.ANGLE;
        else if (type == "tempo")
            enumType = TutorialData.TUTORIAL_TYPE.TEMPO;

        if (TutorialDataList.ContainsKey(enumType))
            TutorialDataList[enumType].Add(data);
        else
        {
            TutorialDataList.Add(enumType, new List<TutorialData>());
            TutorialDataList[enumType].Add(data);
        }
    }

    public void AddAlarmData(AlarmData data)
    {
        AlarmDataList.Add(data);
    }
}