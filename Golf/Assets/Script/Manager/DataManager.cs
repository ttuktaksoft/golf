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
    public List<EvaluationData> EvaluationDataList = new List<EvaluationData>();
    public List<GiftconData> GiftconDataList = new List<GiftconData>();
    public List<PracticeData> PracticeDataList = new List<PracticeData>();

    public void init()
    {
        AlarmDataList.Add(new AlarmData("1월 아카데미 특강안내", DateTime.Now.Ticks, CommonData.TEMP_ALARM_MSG[0]));
        AlarmDataList.Add(new AlarmData("2월 아카데미 특강안내", DateTime.Now.Ticks, CommonData.TEMP_ALARM_MSG[1]));
        AlarmDataList.Add(new AlarmData("3월 아카데미 특강안내", DateTime.Now.Ticks, CommonData.TEMP_ALARM_MSG[2]));

        Dictionary<CommonData.TRAINING_ANGLE, int> angleList = new Dictionary<CommonData.TRAINING_ANGLE, int>();
        angleList.Add(CommonData.TRAINING_ANGLE.TRAINING_ANGLE_BEND, 1);
        angleList.Add(CommonData.TRAINING_ANGLE.TRAINING_ANGLE_SIDE, 2);
        angleList.Add(CommonData.TRAINING_ANGLE.TRAINING_ANGLE_TURN, 3);

        EvaluationDataList.Add(
            new EvaluationData(
                DateTime.Now.Ticks,
                CommonData.TRAINING_TYPE.TRAINING_POSE,
                20,
                CommonData.TRAINING_POSE.TRAINING_ADDRESS,
                angleList,
                3,
                "최고의 홈 트레이닝 앱 입니다.!")
            );

        EvaluationDataList.Add(
            new EvaluationData(
                DateTime.Now.Ticks,
                CommonData.TRAINING_TYPE.TRAINING_POSE,
                20,
                CommonData.TRAINING_POSE.TRAINING_BACKSWING,
                angleList,
                3,
                "매우 좋아요!!")
            );

        EvaluationDataList.Add(
            new EvaluationData(
                DateTime.Now.Ticks,
                CommonData.TRAINING_TYPE.TRAINING_POSE,
                20,
                CommonData.TRAINING_POSE.TRAINING_IMPACT,
                angleList,
                3,
                "힘들다!")
            );

        EvaluationDataList.Add(
            new EvaluationData(
                DateTime.Now.Ticks,
                CommonData.TRAINING_TYPE.TRAINING_TEMPO,
                20,
                3,
                "힘들다!")
            );

        GiftconDataList.Add(new GiftconData("스타벅스 아메리카노", ""));
        GiftconDataList.Add(new GiftconData("스타벅스 아메리카노", ""));
        GiftconDataList.Add(new GiftconData("스타벅스 아메리카노", ""));
        GiftconDataList.Add(new GiftconData("스타벅스 아메리카노", ""));
        GiftconDataList.Add(new GiftconData("스타벅스 아메리카노", ""));

        PracticeDataList.Add(new PracticeData(CommonData.TRAINING_TYPE.TRAINING_POSE, CommonData.TRAINING_POSE.TRAINING_ADDRESS, 10));
        PracticeDataList[0].SetTodayPracticeCount(5);
        PracticeDataList.Add(new PracticeData(CommonData.TRAINING_TYPE.TRAINING_POSE, CommonData.TRAINING_POSE.TRAINING_BACKSWING, 30));
        PracticeDataList[1].SetTodayPracticeCount(15);
        PracticeDataList.Add(new PracticeData(CommonData.TRAINING_TYPE.TRAINING_POSE, CommonData.TRAINING_POSE.TRAINING_IMPACT, 50));
        PracticeDataList[2].SetTodayPracticeCount(10);
        PracticeDataList.Add(new PracticeData(CommonData.TRAINING_TYPE.TRAINING_TEMPO, 80));
        PracticeDataList[3].SetTodayPracticeCount(3);

    }
}