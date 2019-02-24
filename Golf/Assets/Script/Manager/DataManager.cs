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
    public List<TutorialData> TutorialDataList = new List<TutorialData>();

    public void init()
    {
        //TKManager.Instance.SetName("이민영");
        //TKManager.Instance.SetPhoneNumber("010-1234-1234");
        TKManager.Instance.SetGender(CommonData.GENDER.GENDER_MAN);
        TKManager.Instance.SetGrade(0);

        AlarmDataList.Add(new AlarmData("새해인사", DateTime.Now.Ticks, "alram_1"));
        AlarmDataList.Add(new AlarmData("아카데미 소개", DateTime.Now.Ticks, "alram_2"));
        AlarmDataList.Add(new AlarmData("아카데미 안내", DateTime.Now.Ticks, "alram_3"));
        AlarmDataList.Add(new AlarmData("어플 사용방법", DateTime.Now.Ticks, "alram_4"));
        AlarmDataList.Add(new AlarmData("정식버전 출시 일정", DateTime.Now.Ticks, "alram_5"));

        //Dictionary<CommonData.TRAINING_ANGLE, int> angleList = new Dictionary<CommonData.TRAINING_ANGLE, int>();
        //angleList.Add(CommonData.TRAINING_ANGLE.TRAINING_ANGLE_BEND, 1);
        //angleList.Add(CommonData.TRAINING_ANGLE.TRAINING_ANGLE_SIDE, 2);
        //angleList.Add(CommonData.TRAINING_ANGLE.TRAINING_ANGLE_TURN, 3);

        //EvaluationDataList.Add(
        //    new EvaluationData(
        //        DateTime.Now.Ticks,
        //        CommonData.TRAINING_TYPE.TRAINING_POSE,
        //        20,
        //        CommonData.TRAINING_POSE.TRAINING_ADDRESS,
        //        angleList,
        //        3,
        //        "최고의 홈 트레이닝 앱 입니다.!")
        //    );

        //EvaluationDataList.Add(
        //    new EvaluationData(
        //        DateTime.Now.Ticks,
        //        CommonData.TRAINING_TYPE.TRAINING_POSE,
        //        20,
        //        CommonData.TRAINING_POSE.TRAINING_BACKSWING,
        //        angleList,
        //        3,
        //        "매우 좋아요!!")
        //    );

        //EvaluationDataList.Add(
        //    new EvaluationData(
        //        DateTime.Now.Ticks,
        //        CommonData.TRAINING_TYPE.TRAINING_POSE,
        //        20,
        //        CommonData.TRAINING_POSE.TRAINING_IMPACT,
        //        angleList,
        //        3,
        //        "힘들다!")
        //    );

        //EvaluationDataList.Add(
        //    new EvaluationData(
        //        DateTime.Now.Ticks,
        //        CommonData.TRAINING_TYPE.TRAINING_TEMPO,
        //        20,
        //        3,
        //        "힘들다!")
        //    );

        GiftconDataList.Add(new GiftconData(1,"스타벅스 아메리카노", ""));
        //GiftconDataList.Add(new GiftconData(2,"스타벅스 아메리카노", ""));
        //GiftconDataList.Add(new GiftconData(3,"스타벅스 아메리카노", ""));
        //GiftconDataList.Add(new GiftconData(4,"스타벅스 아메리카노", ""));
        //GiftconDataList.Add(new GiftconData(5, "스타벅스 아메리카노", ""));

        if (PracticeDataList.Count <= 0)
        {
            PracticeDataList.Add(new PracticeData(CommonData.TRAINING_TYPE.TRAINING_POSE, CommonData.TRAINING_POSE.TRAINING_ADDRESS, 0));
            PracticeDataList.Add(new PracticeData(CommonData.TRAINING_TYPE.TRAINING_POSE, CommonData.TRAINING_POSE.TRAINING_BACKSWING, 0));
            PracticeDataList.Add(new PracticeData(CommonData.TRAINING_TYPE.TRAINING_POSE, CommonData.TRAINING_POSE.TRAINING_IMPACT, 0));
            PracticeDataList.Add(new PracticeData(CommonData.TRAINING_TYPE.TRAINING_TEMPO, 0));
        }
        

        TutorialDataList.Add(new TutorialData("PGA 프로의 연습 방법", "https://blog.naver.com/golfmanager/221401601938"));
        TutorialDataList.Add(new TutorialData("드라이버 장타를 위한 연습", "https://blog.naver.com/golfmanager/221338934464"));
    }

    public void RemoveGiftcon(int index)
    {
        for (int i = 0; i < GiftconDataList.Count; i++)
        {
            if (GiftconDataList[i].Index == index)
            {
                GiftconDataList.RemoveAt(i);
                break;
            }
        }
    }

    public void AddEvaluationData(EvaluationData data)
    {
        EvaluationDataList.Insert(0, data);
        TKManager.Instance.SaveFile();
    }

    public void AddPracticeData(CommonData.TRAINING_TYPE trainingType, CommonData.TRAINING_POSE poseType = CommonData.TRAINING_POSE.TRAINING_ADDRESS)
    {
        for (int i = 0; i < PracticeDataList.Count; i++)
        {
            if(PracticeDataList[i].TrainingType == CommonData.TRAINING_TYPE.TRAINING_TEMPO &&
                trainingType == CommonData.TRAINING_TYPE.TRAINING_TEMPO)
            {
                PracticeDataList[i].AddPracticeCount();
                break;
            }
            else if(PracticeDataList[i].TrainingType == trainingType &&
                PracticeDataList[i].TrainingPoseType == poseType)
            {
                PracticeDataList[i].AddPracticeCount();
                break;
            }
        }

        TKManager.Instance.SaveFile();
    }
}