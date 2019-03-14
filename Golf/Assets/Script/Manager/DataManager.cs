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
    public List<TutorialData> TutorialDataList = new List<TutorialData>();

    public void init()
    {
        //TKManager.Instance.SetName("이민영");
        //TKManager.Instance.SetPhoneNumber("010-1234-1234");
        TKManager.Instance.Mydata.SetGender(CommonData.GENDER.GENDER_MAN);

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

        TutorialDataList.Add(new TutorialData("PGA 프로의 연습 방법", "https://blog.naver.com/golfmanager/221401601938"));
        TutorialDataList.Add(new TutorialData("드라이버 장타를 위한 연습", "https://blog.naver.com/golfmanager/221338934464"));
    }
}